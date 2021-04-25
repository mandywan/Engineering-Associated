import React, {useEffect, useState} from "react";
import {KeyboardDatePicker, MuiPickersUtilsProvider} from '@material-ui/pickers';
import DateFnsUtils from "@date-io/date-fns";
import filters from "../../../services/filters";
import {Button, Grid, Paper, TextField, Modal, makeStyles, Box} from '@material-ui/core';
import DropDown from "./dropdown";
import {useForm} from "./useForm";
import storage from "../../../services/storage";
import {addContractor, formatContractor, formatEditContractor, editContractor} from "../../../services/contractor";
import ImageUpload from "../image-upload";
import { useHistory } from "react-router-dom";
// modal styling
const useStyles = makeStyles((theme) => ({
    modal: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
    },
    paper: {
        backgroundColor: theme.palette.background.paper,
        border: '2px solid #000',
        boxShadow: theme.shadows[5],
        padding: theme.spacing(2, 4, 3),
    },
}));

const isValidDate = (d) => {
    if (d instanceof Date && !isNaN(d.valueOf())) {
        return d > new Date(1900, 0, 0) && d < new Date(2100, 0, 0)
    }
    return false
}

const verifyMaxLen = (str, len) => {
    if (str && str.length > len) {
        return "Exceeded maximum character length"
    }
    return ""
}

// Model texts
const ADD_SUCCESS_TITLE = "Add Success";
const ADD_SUCCESS_TEXT = "Contractor was added successfully."
const ADD_FAIL_TITLE = "Add Failed";
const ADD_FAIL_TEXT = "Failed to add the contractor. Please enter a valid supervisor Id."

const EDIT_SUCCESS_TITLE = "Edit Success";
const EDIT_SUCCESS_TEXT = "Contractor was edited successfully."
const EDIT_FAIL_TITLE = "Edit Failed";
const EDIT_FAIL_TEXT = "Failed to edit the contractor. Please ensure all information is entered correctly."

// TODO: refactor modal
const ContractorForm = (props) => {
    let history = useHistory();
    const classes = useStyles();
    //const formTitle = props.data.hasData ? "Edit Contractor" : "Add a contractor";
    const isEdit = Object.keys(props.data).length !== 0
    const formTitle = isEdit ? "Edit contractor" : "Add a contractor";
    const [companies, setCompanies] = useState([]);
    const [offices, setOffices] = useState([{value_id: -1, value_name: "Please select a Company first"}]);
    const [groups, setGroups] = useState([{value_id: -1, value_name: "Please select an Office first"}]);
    const [locations, setLocations] = useState([]);
    const [open, setOpen] = useState(false);
    const [modalText, setModalText] = useState("");
    const [modalTitle, setModalTitle] = useState("");
    const [imageName, setImageName] = useState("")
    const [defaultCompany, setDefaultCompany] = useState("")
    const [defaultOffice, setDefaultOffice] = useState("")
    const [defaultGroup, setDefaultGroup] = useState("")
    const [defaultLoc, setDefaultLoc] = useState("")
    const [hireDateError, setHireDateError] = useState("")
    const [termDateError, setTermDateError] = useState("")

    // initial form values
    let initialFValues = {
        lastName: props.data.lastName || "",
        firstName: props.data.firstName || "",
        companyCode: "",
        officeCode: "",
        groupCode: "",
        locationId: props.data.locationId || "",
        supervisorEmployeeNumber: props.data.supervisorEmployeeNumber || "",
        employmentType: props.data.employmentType || "",
        title: props.data.title || "",
        yearsPriorExperience: props.data.yearsPriorExperience || "",
        email: props.data.email || "",
        workPhone: props.data.workPhone || "",
        workCell: props.data.workCell || "",
        hireDate: isEdit ? new Date(props.data.hireDate) : null,
        terminationDate: isEdit ? new Date(props.data.terminationDate) : null,
        bio: props.data.bio || "",
        extraInfo: props.data.extraInfo || "",
    }

    // set the initial companies and locations selections
    useEffect(async() => {
        setCompanies(await storage.db.searchDocument('metadata', {call_name: 'Company'}));
        setLocations(await storage.db.searchDocument('metadata', {call_name: 'Location'}));

        if (isEdit) {
            let company = await storage.db.searchDocument('metadata', {meta_id: `Company,${props.data.companyCode}`})
            setDefaultCompany(company[0].value_name)
            let office = await storage.db.searchDocument('metadata', {meta_id: `Office,${props.data.companyCode},${props.data.officeCode}`})
            setDefaultOffice(office[0].value_name)
            let group = await storage.db.searchDocument('metadata', {meta_id: `Group,${props.data.companyCode},${props.data.officeCode},${props.data.groupCode}`})
            setDefaultGroup(group[0].value_name)
            let location = await storage.db.searchDocument('metadata', {meta_id: `Location,${props.data.locationId}`})
            setDefaultLoc(location[0].value_name)
        }
    }, [])


    // validation logic
    const validate = (fieldValues = values) => {
        let temp = { ...errors }

        if ('lastName' in fieldValues)
            temp.lastName = fieldValues.lastName ? "" : "This field is required."
            if (temp.lastName === "") {
                temp.lastName = verifyMaxLen(fieldValues.lastName, 50)
            }
        if ('firstName' in fieldValues)
            temp.firstName = fieldValues.firstName ? "" : "This field is required."
            if (temp.firstName === "") {
                temp.firstName = verifyMaxLen(fieldValues.firstName, 25)
            }
        if ('companyCode' in fieldValues && !isEdit)
            temp.companyCode = fieldValues.companyCode ? "" : "This field is required."
        if ('officeCode' in fieldValues && !isEdit)
            temp.officeCode = fieldValues.officeCode && fieldValues.officeCode !== -1 ? "" : "This field is required."
        if ('groupCode' in fieldValues && !isEdit)
            temp.groupCode = fieldValues.groupCode && fieldValues.groupCode !== -1 ? "" : "This field is required."
        if ('locationId' in fieldValues)
            temp.locationId = fieldValues.locationId && fieldValues.locationId !== -1 ? "" : "This field is required."
        if ('supervisorEmployeeNumber' in fieldValues)
            temp.supervisorEmployeeNumber = fieldValues.supervisorEmployeeNumber ? "" : "This field is required."
        if ('email' in fieldValues)
            temp.email = (/$^|.+@.+..+/).test(fieldValues.email) ? "" : "Email is not valid."
            if (temp.email === "") {
                temp.email = verifyMaxLen(fieldValues.email)
            }
        if ('workPhone' in fieldValues)
            temp.workPhone = fieldValues.workPhone.length === 0 || (/^\d{3}\-\d{3}\-\d{4}$/).test(fieldValues.workPhone) ? "" : "Phone number is not valid."
        if ('workCell' in fieldValues)
            temp.workCell = fieldValues.workCell.length === 0 || (/^\d{3}\-\d{3}\-\d{4}$/).test(fieldValues.workCell) ? "" : "Phone number is not valid."
        if ('yearsPriorExperience' in fieldValues)
            temp.yearsPriorExperience = fieldValues.yearsPriorExperience >= 0 ? "" : "Please enter a number greater than or equal to zero."
        if ('hireDate' in fieldValues)
            if (fieldValues.hireDate && !isValidDate(fieldValues.hireDate)) {
                setHireDateError("Please enter a valid date")
            } else if (fieldValues.hireDate
                && values.terminationDate
                && isValidDate(values.terminationDate)
                && fieldValues.hireDate >= values.terminationDate) {
                setHireDateError("Invalid hire date: Hire date should be earlier than termination date.")
            } else {
                setHireDateError("")
                setTermDateError("")
            }
        if ('terminationDate' in fieldValues)
            if (fieldValues.terminationDate && !isValidDate(fieldValues.terminationDate)) {
                setTermDateError("Please enter a valid date")
            } else if (fieldValues.terminationDate
                && values.hireDate
                && isValidDate(values.hireDate)
                && values.hireDate >= fieldValues.terminationDate) {
                setTermDateError("Invalid termination date: Termination date should be later than hire date.")
            } else {
                setTermDateError("")
                setHireDateError("")
            }
        if ('employmentType' in fieldValues)
            temp.employmentType = verifyMaxLen(fieldValues.employmentType, 10)
        if ('title' in fieldValues)
            temp.title = verifyMaxLen(fieldValues.employmentType, 50)

        setErrors({
            ...temp
        })

        if (fieldValues === values)
            return Object.values(temp).every(x => x === "")
    }

    // get useForm hooks
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange,
        resetForm
    } = useForm(initialFValues, true, validate);

    // set lower hierarchy members' selection when a hierarchy member is changed
    const handleHierarchyChange = async(e) => {
        const { name, value } = e.target
        switch (name) {
            case "companyCode":
                setOffices(await filters.getChildFromAncestor("Office", value));
                break;
            case "officeCode":
                if (value !== -1) {
                    setGroups(await filters.getChildFromAncestor("Group", value));
                }
                break;
            default:
                break;
        }

        setValues({
            ...values,
            [name]: value
        })
    }

    const handleNotLoggedIn = () => {
        alert("Please login first")
        history.push(`/login`);
        window.location.reload();
    }

    // submit function
    const handleSubmit = e => {
        e.preventDefault()
        if (validate()) {
            const requestBody = formatContractor(values, imageName);
            const editRequestBody = formatEditContractor(values, imageName);
            // case add - no initial data
            if (!isEdit) {
                addContractor(requestBody).then(res => {
                    if (res.status === 200) {
                        setModalTitle(ADD_SUCCESS_TITLE);
                        setModalText(ADD_SUCCESS_TEXT);
                        handleOpen();
                        history.push(`/admin`);
                        window.location.reload();
                    } else if (res.response.status === 401) {
                        handleNotLoggedIn()
                    } else {
                        setModalTitle(ADD_FAIL_TITLE);
                        setModalText(ADD_FAIL_TEXT);
                        // setModalText(res.response.data.title)
                        handleOpen();
                    }
                });
            } else {
                editContractor(props.data.employeeNumber, editRequestBody).then(res => {
                    if (res.status === 200) {
                        setModalTitle(EDIT_SUCCESS_TITLE);
                        setModalText(EDIT_SUCCESS_TEXT);
                        handleOpen();
                    } else if (res.response.status === 401) {
                        handleNotLoggedIn()
                    } else {
                        setModalTitle(EDIT_FAIL_TITLE);
                        setModalText(EDIT_FAIL_TEXT);
                        // setModalText(res.response.data.title)
                        handleOpen();
                    }
                });
            }
        }
    }

    // for date
    const convertToDefEventPara = (name, value) => ({
        target: {
            name, value
        }
    })

    // modal controls
    const handleOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleBack = () => {
        // pushes the contractor data onto edit page url
        history.push(`/admin`);
        window.location.reload();
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>{formTitle}</h2>
            <Modal
                open={open}
                onClose={handleClose}
                className={classes.modal}
                aria-labelledby="simple-modal-title"
                aria-describedby="simple-modal-description"
            >
                <div className={classes.paper}>
                    <h2 id="transition-modal-title">{modalTitle}</h2>
                    <p id="transition-modal-description">{modalText}</p>
                </div>
            </Modal>
            <Paper style={{ padding: 16 }}>
                <Grid container spacing={5} >
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth={true}
                            required
                            label="Last Name"
                            name="lastName"
                            value={values.lastName}
                            onChange={handleInputChange}
                            error =  {errors.lastName}
                            helperText={errors.lastName}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth={true}
                            required
                            label="First Name"
                            name="firstName"
                            value={values.firstName}
                            onChange={handleInputChange}
                            error =  {errors.firstName}
                            helperText={errors.firstName}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <DropDown
                            name="companyCode"
                            label="Company"
                            placeHolder={defaultCompany}
                            value={isEdit ? "" : values.companyCode.value_name}
                            onChange={handleHierarchyChange}
                            options={companies}
                            error =  {errors.companyCode}
                            helperText={errors.companyCode}
                            disabled = {isEdit}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <DropDown
                            name="officeCode"
                            label="Office"
                            placeHolder={defaultOffice}
                            value={isEdit ? "" : values.officeCode.value_name}
                            onChange={handleHierarchyChange}
                            options={offices}
                            error =  {errors.officeCode}
                            helperText={errors.officeCode}
                            disabled = {isEdit}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <DropDown
                            name="groupCode"
                            label="Group"
                            placeHolder={defaultGroup}
                            value={isEdit ? "" : values.groupCode.value_name}
                            onChange={handleInputChange}
                            options={groups}
                            error =  {errors.groupCode}
                            helperText={errors.groupCode}
                            disabled = {isEdit}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <DropDown
                            required
                            name="locationId"
                            label="Physical Location"
                            placeHolder={defaultLoc}
                            value={isEdit ? "" : values.locationId.value_name}
                            onChange={handleInputChange}
                            options={locations}
                            error =  {errors.locationId}
                            helperText={errors.locationId}
                            disabled = {isEdit}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth={true}
                            label="Email Address"
                            name="email"
                            value={values.email}
                            onChange={handleInputChange}
                            error =  {errors.email}
                            helperText={errors.email}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth={true}
                            label="Work phone"
                            name="workPhone"
                            value={values.workPhone}
                            onChange={handleInputChange}
                            error =  {errors.workPhone}
                            helperText={"Format: xxx-xxx-xxxx"}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth={true}
                            label="Work cell"
                            name="workCell"
                            value={values.workCell}
                            onChange={handleInputChange}
                            error =  {errors.workCell}
                            helperText={"Format: xxx-xxx-xxxx"}
                        />
                    </Grid>
                    <Grid item xs={12} sm={3}>
                        <TextField
                            fullWidth={true}
                            required
                            type="number"
                            label="Supervisor Employee No."
                            name="supervisorEmployeeNumber"
                            value={values.supervisorEmployeeNumber}
                            onChange={handleInputChange}
                            error =  {errors.supervisorEmployeeNumber}
                            helperText={errors.supervisorEmployeeNumber}
                            disabled={isEdit}
                        />
                    </Grid>
                    <Grid item xs={12} sm={3}>
                        <TextField
                            fullWidth={true}
                            type="number"
                            label="Years of Prior Experience"
                            name="yearsPriorExperience"
                            value={values.yearsPriorExperience}
                            onChange={handleInputChange}
                            error =  {errors.yearsPriorExperience}
                            helperText={errors.yearsPriorExperience}
                        />
                    </Grid>
                    <Grid item xs={12} sm={3}>
                        <TextField
                            fullWidth={true}
                            label="Title"
                            name="title"
                            value={values.title}
                            onChange={handleInputChange}
                            error = {errors.title}
                            helperText = {errors.title}
                        />
                    </Grid>
                    <Grid item xs={12} sm={3}>
                        <TextField
                            fullWidth={true}
                            label="Employment Type"
                            name="employmentType"
                            value={values.employmentType}
                            onChange={handleInputChange}
                            error = {errors.employmentType}
                            helperText = {errors.employmentType}
                        />
                    </Grid>
                    <Grid item xs={12} sm={3}>
                        <MuiPickersUtilsProvider utils={DateFnsUtils}>
                            <KeyboardDatePicker disableToolbar variant="inline" inputVariant="outlined"
                                                label="Hire Date"
                                                format="MM/dd/yyyy"
                                                placeholder="mm/dd/yyyy"
                                                name="hireDate"
                                                value={values.hireDate}
                                                onChange={date =>handleInputChange(convertToDefEventPara('hireDate',date))}
                                                error = {hireDateError.length > 0}
                                                helperText = {hireDateError}

                            />
                        </MuiPickersUtilsProvider>
                    </Grid>
                    <Grid item xs={12} sm={3}>
                        <MuiPickersUtilsProvider utils={DateFnsUtils}>
                            <KeyboardDatePicker disableToolbar variant="inline" inputVariant="outlined"
                                                label="Termination Date"
                                                format="MM/dd/yyyy"
                                                name="terminationDate"
                                                placeholder="mm/dd/yyyy"
                                                value={values.terminationDate}
                                                onChange={date =>handleInputChange(convertToDefEventPara('terminationDate',date))}
                                                error = {termDateError.length > 0}
                                                helperText = {termDateError}

                            />
                        </MuiPickersUtilsProvider>
                    </Grid>
                    <Grid item xs={12}>
                        <h4>Upload Image: </h4>
                        <ImageUpload passImageName={setImageName}/>
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            fullWidth={true}
                            name="bio"
                            label="Biography"
                            onChange={handleInputChange}
                            multiline
                            rows={2}
                            value={values.bio}
                            variant="outlined"
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            fullWidth={true}
                            name="extraInfo"
                            label="Extra Information"
                            onChange={handleInputChange}
                            multiline
                            rows={4}
                            value={values.extraInfo}
                            variant="outlined"
                        />
                    </Grid>
                    <Box p={2}>
                        <Button
                            variant={"contained"}
                            size={"large"}
                            color={"primary"}
                            onClick={handleSubmit}
                            text={"Submit"}>
                            Submit
                        </Button>
                    </Box>
                    <Box p={2}>
                        <Button
                            variant={"contained"}
                            size={"large"}
                            color={"primary"}
                            onClick={resetForm}
                            text={"Reset"}>
                            Reset
                        </Button>
                    </Box>
                    <Box p={2}>
                        <Button
                            variant={"contained"}
                            size={"large"}
                            color={"primary"}
                            onClick={handleBack}
                            text={"Back to Admin Page"}>
                            Back
                        </Button>
                    </Box>
                </Grid>
            </Paper>
        </form>
    );
};

export default ContractorForm