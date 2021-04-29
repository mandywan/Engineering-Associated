import axios from 'axios';
import jwtManager from "./jwt-manager"

const contractor = {};

// formats the contractor object for http request
export const formatContractor = (contractor, imageFileName) => {
    // make a deep copy
    let res = JSON.parse(JSON.stringify(contractor));
    for (let [key, value] of Object.entries(res)) {
        // delete empty fields
        if (value === "" || value === null) {
            delete res[key];
        // format dropdown items
        } else if (key === "companyCode" || key === "officeCode" || key === "groupCode" || key === "locationId" ) {
            res[key] = res[key][value.length-1]
        // format number fields
        } else if (key === "supervisorEmployeeNumber" || key === "yearsPriorExperience") {
            res[key] = parseInt(value)
        }
    }

    // update image url
    if (imageFileName !== "") {
        res["photoUrl"] = imageFileName;
    }
    return res;
}

// send addContractor request
export const addContractor = (contractor) => {
    let config = {
        headers: {
            'Authorization': 'Bearer ' + jwtManager.getToken()
        }
    }
    return axios.post('/api/contractors', contractor, config)
        .then((response) => {
                // return response.data;
                return response;
            },
            (error) => {
                console.log(error);
                return error;
            }
        );
};

// formats the contractor object for put http request
export const formatEditContractor = (contractor, imageFileName, expVal) => {
    // make a deep copy
    let res = JSON.parse(JSON.stringify(contractor));
    for (let [key, value] of Object.entries(res)) {
        // years of experience has to be filled
        // format yearsPriorExperience
        if (key === "yearsPriorExperience") {
            if (value === "") {
                if (expVal === "") {
                    delete res[key];
                } else {
                    res[key] = 0;
                }
            } else {
                res[key] = parseInt(value)
            }
            // remove all the fields we disabled
        } else if (key === "companyCode" || key === "officeCode" || key === "groupCode" || key === "locationId" || key === "supervisorEmployeeNumber" ) {
            delete res[key];   
        } 
    }

    // update image url
    if (imageFileName !== "") {
        res["photoUrl"] = imageFileName;
    }
    return res;
}

// send editContractor request
export const editContractor = async(id, contractor) => {
    let config = {
        headers: {
            'Authorization': 'Bearer ' + jwtManager.getToken()
        }
    }
     
    return axios.put('/api/contractors/' + id, contractor, config)
        .then((response) => {
                // return response.data;
                return response;
            },
            (error) => {
                console.log(error);
                return error;
            }
        );
};

contractor.getAllContractors = async () => {
    let config = {
        headers: {
            'Authorization': 'Bearer ' + jwtManager.getToken()
        }
    }
    const res = await new Promise(async (resolve, reject) => {
        return axios.get("/api/contractors", config).then(
            async (response) => {
                let results = response.data;
                resolve(results);
            },
            (error) => {
                console.log(error);
                reject(error)
            }
        );
    });

    return res;
}


export default contractor;
