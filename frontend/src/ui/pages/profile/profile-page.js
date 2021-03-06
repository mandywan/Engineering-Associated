import React, { useEffect, useState } from "react";
import PageHeader from "../../components/header";
import PageTitle from "../../components/page-title-banner";
import ProfileCardLarge from "../../components/profile-card-large";
import ProfileAccordion from "../../components/profile-accordion";
import Footer from "../../components/footer/footer";
import { getProfileResults } from "../../../services/profile";
import "./profile-page.css";
import { useLocation, useParams } from "react-router-dom";
import ProfileCard from "../../components/profile-card";
import { withStyles } from "@material-ui/core";
import Typography from "@material-ui/core/Typography";
import { makeStyles } from "@material-ui/core/styles";
import SkillsAccordion from "../../components/skills-accordion";
import storage from "../../../services/storage";
import EventEmitter from "../../hooks/event-manager";

const HeaderTypography = withStyles({
    root: {
        color: "#26415C",
        fontWeight: 600,
        fontSize: 30,
        fontFamily: "Poppins",
    },
})(Typography);

const ProfilePage = (props) => {
    const heading_text = "Employee Profile";
    const location = useLocation();
    let { id } = useParams(); // dynamic part of url, in this case, employeeNumber

    const [profileResults, setProfileResults] = useState({
        extraInfo: "...",
        hiredOn: "...",
        groupAndOffice: "...",
        bio: "...",
        firstName: "...",
        title: "...",
        workCell: "...",
        employmentType: "...",
        email: "...",
    });
    const [skills, setSkills] = useState([]);
    const [supervisorResults, setSupervisorResults] = useState([]);

    const [isLoading, setIsLoading] = useState(true);

    useEffect(async () => {
        EventEmitter.emit('Loading');
        getProfileResults(id).then(async(res) => {
            let hiredDate = new Date(res.hireDate);
            let month = [
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December",
            ];
            res.hiredOn =
                "Associated since " +
                month[hiredDate.getMonth()] +
                " " +
                hiredDate.getDate() +
                ", " +
                hiredDate.getFullYear();
            if (res.terminationDate !== null) {
                let terminationDate = new Date(res.terminationDate)
                res.hiredOn =
                    "Associated from " +
                    month[hiredDate.getMonth()] +
                    " " +
                    hiredDate.getFullYear() +
                    " to " +
                    month[terminationDate.getMonth()] +
                    " " +
                    terminationDate.getFullYear();
            }
            res.groupAndOffice = res.groupName + "(" + res.officeName + ")";
            setProfileResults(res);
            setSkills(res.skills);
            res.supervisor.employeeNumber = res.supervisorEmployeeNumber;
            res.supervisor.groupCode = res.supervisor.group_id;
            res.supervisor.officeCode = res.supervisor.office_id;
            setSupervisorResults(res.supervisor);

            await storage.db.updateDocuments('viewHistory',[{
                employeeNumber: res.employeeNumber,
                title: res.title,
                groupName: res.groupName,
                lastName: res.lastName,
                firstName: res.firstName,
                email: res.email,
                workCell: res.workCell
            }]);
        });
    }, [location]);
    // Control loading indicator
    // Based on availability of first name, could be buggy
    useEffect(() => {
        if (
            profileResults.firstName !== null &&
            profileResults.firstName !== "..."
        ) {
            setIsLoading(false);
            EventEmitter.emit('Loaded');
        }
    }, [profileResults]);

    return (
        <div className={"page"}>
            <PageTitle data={{ title: heading_text }} />
            <div className="page-contents-wrapper">
                {isLoading ? (
                    null
                ) : (
                    <div className="page-contents-container">
                        <ProfileCardLarge data={profileResults} />
                        {profileResults.employeeNumber !==
                        supervisorResults.employeeNumber ? (
                            <div className={"reporting-manager-box"}>
                                <div className={"title"}>
                                    <HeaderTypography align={"left"}>
                                        Reporting Managers
                                    </HeaderTypography>
                                </div>
                                <div className={"content profile-page-cards"}>
                                    <ProfileCard data={supervisorResults} tab="override"/>
                                </div>
                            </div>
                        ) : (
                            <div className={"reporting-manager-box"} />
                        )}
                        <SkillsAccordion data={skills} />
                        <ProfileAccordion
                            title={"Extra Information"}
                            description={profileResults.extraInfo}
                        />
                    </div>
                )}
            </div>
        </div>
    );
};

export default ProfilePage;