using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AeDirectory.DTO;
using AeDirectory.Models;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace AeDirectory.Services
{
    public interface IStorageService
    {
        Task<bool> AddItem(IFormFile file, string employeeID, int id);
        Task<Stream> GetItem(int id);
    }

    public class S3StorageService : IStorageService
    {
        private readonly AmazonS3Client s3Client;
        private const string BUCKET_NAME = "ae319";
        private const string FOLDER_NAME = "images";
        private readonly IContractorService _contractorService;
        private readonly IEmployeeService _employeeService;
        // private static string accessKey = "YOUR_ACCESS_KEY_ID";
        // private static string accessSecret = "YOUR_SECRET_ACCESS_KEY";

        public S3StorageService(IContractorService contractorService, IEmployeeService employeeService)
        {
            s3Client = new AmazonS3Client(RegionEndpoint.USEast1);
            _contractorService = contractorService;
            _employeeService = employeeService;
        }

        //https://docs.aws.amazon.com/zh_cn/AmazonS3/latest/dev-retired/HLuploadFileDotNet.html
        public async Task<bool> AddItem(IFormFile file, string fileName, int id)
        {
            // string fileName = file.FileName;
            // string objectKey = $"{FOLDER_NAME}/{readerName}/{fileName}";

            string objectKey = $"{FOLDER_NAME}/{fileName}";

            using (Stream fileToUpload = file.OpenReadStream())
            {
                var putObjectRequest = new PutObjectRequest();
                putObjectRequest.BucketName = BUCKET_NAME;
                putObjectRequest.Key = objectKey;
                putObjectRequest.InputStream = fileToUpload;
                putObjectRequest.ContentType = file.ContentType;

                // save the photo to S3 bucket
                var response = await s3Client.PutObjectAsync(putObjectRequest);

                // update photo url
                ContractorUpdateDTO updateDto = new ContractorUpdateDTO();
                updateDto.PhotoUrl = objectKey;
                _contractorService.UpdateContractor(updateDto, id);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
        }

        public async Task<Stream> GetItem(int id)
        {
            // get photo URL from DB
            // eg. objectKey = "images/2.jpg";
            string objectKey = _employeeService.GetEmployeeByEmployeeNumber(id).PhotoUrl;
            
            // async get photo from S3
            GetObjectResponse response = await s3Client.GetObjectAsync(BUCKET_NAME, objectKey);
            
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return response.ResponseStream;
            else
                return null;
        }
    }
}