import axios from 'axios';

export const getOrgChartResults = (employeeID) => {
    return axios.get('/api/orgchart/' + employeeID)
        .then((response) => {
                return response.data;
            },
            (error) => {
                console.log(error);
            }
        );
};