import React, {useState} from 'react'
import {Button, Box} from "@material-ui/core";
import {uploadImage} from "../../../services/image-upload";
import axios from "axios";
import CheckCircleOutlineSharpIcon from '@material-ui/icons/CheckCircleOutlineSharp';

const ImageUpload = (props) => {
    const [selectedFile, setSelectedFile] = useState(null);
    const fileInput = React.useRef(null);
    const [isUploading, setIsUploading] = useState(false)
    const [isUploaded, setIsUploaded] = useState(false)

    const fileChangedHandler = (event) => {
        // cannot be larger than 2M
        if(event.target.files[0].size > 2097152){
            alert("File is too big! Maximum file size is 2MB.");
        } else {
            setSelectedFile(event.target.files[0]);
            setIsUploaded(false);
        }
    }

    const handleClick = (event) => {
        fileInput.current.click();
    }

    // TODO: ux improvement during file upload
    const uploadHandler = (event) => {
        // create unique fileName with time stamp. fileName is used as key for s3 db
        setIsUploading(true)
        const keys = selectedFile.name.split('.');
        const fileName = new Date().getTime().toString() + "." + keys[keys.length-1];

        const fd = new FormData();
        fd.append("photoFile", selectedFile, fileName)

        uploadImage(fd).then(res => {
            if (res.status === 200) {
                // update imageName in parent component
                props.passImageName(res.data)
                setIsUploaded(true);
            } else {
                alert("File upload unsuccessful. Please try again.")
            }
            setIsUploading(false)
        })
    }


    return (
        <div>
            <input
                style={{display: 'none'}}
                type="file"
                accept="image/*"
                onChange={fileChangedHandler}
                ref={fileInput}/>
            <Box py={2}>
                <Button
                    variant={"contained"}
                    size={"medium"}
                    color={"primary"}
                    onClick={handleClick}
                    text={"Choose an image"}>
                    Choose an image
                </Button>
                {selectedFile ? selectedFile.name : "No file selected"}
            </Box>
            <Box>
                <Button
                    disabled={!selectedFile || isUploading}
                    variant={"contained"}
                    size={"medium"}
                    color={"primary"}
                    onClick={uploadHandler}
                    text={"Upload"}>
                    Upload
                </Button>
                <CheckCircleOutlineSharpIcon
                    style={{ display: isUploaded ? 'inline' : 'none' }}
                    color="secondary"
                    >
                </CheckCircleOutlineSharpIcon>
            </Box>
        </div>
    );
};

export default ImageUpload;
