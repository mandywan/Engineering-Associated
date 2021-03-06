import React, { useState, useEffect } from "react";
import {
    CardActionArea,
    Typography,
    CardContent,
    Card,
    IconButton,
    CardActions,
    Popover,
} from "@material-ui/core";
import CloseIcon from "@material-ui/icons/Close";
import { useHistory } from "react-router-dom";
import "./search-card.css";
import search from "../../../services/search";
import storage from "../../../services/storage";
import { makeStyles } from "@material-ui/core/styles";
import EventEmitter from "../../hooks/event-manager";

const useStyles = makeStyles((theme) => ({
    popover: {
        pointerEvents: "none",
    },
    paper: {
        padding: theme.spacing(1),
    },
}));

const SearchCard = (props) => {
    let history = useHistory();

    const [uri, setURI] = useState(props.data.uri);
    const [decodedUri, setDecodedUri] = useState(null);
    const [data, setData] = useState([]);
    const [skillType, setSkillType] = useState('AND');

    useEffect(async () => {
        let temp = await parseUri();
        setData(temp);
    }, []);


    async function parseUri() {
        return new Promise(async (res) => {

            let allData;
            let decodedURI = JSON.parse(decodeURIComponent(props.data.uri));
            let decodedSkillType;
            setDecodedUri(decodedURI);

            if (Object.keys(decodedURI).includes('Skill')) {
                decodedSkillType = decodedURI.Skill.type;
                setSkillType(decodedURI.Skill.type);
            }

            if (decodedURI.meta) {
                // if there are filters
                if (decodedURI.meta.length > 0) {
                    allData = await parseObjectWithMetas();
                    // if there are no filters
                } else {
                    allData = await parseObjectNoMetas();
                }
            }

            async function parseObjectNoMetas() {
                let basisKeyName = JSON.parse(props.data.basisKeyName);
                let obj = {
                    hasFilters: false,
                    hasBasisKeyName: true,
                    name: props.data.name,
                    keyName: basisKeyName,
                };
                return obj;
            }

            async function parseObjectWithMetas() {
                let selectionRaw = decodedURI.meta;
                let filtersData = [];
                let skills = [];
                
                // only push one meta id
                // check if it is has a double underscore, which means duplicate group
                await createFiltersData();
                let basisKeyName = JSON.parse(props.data.basisKeyName);
                let obj = {
                    hasFilters: true,
                    hasBasisKeyName: basisKeyName.key !== null &&
                        basisKeyName.name !== null,
                    hasMoreThanOneSkill: skills.length > 1,
                    filters: filtersData,
                    name: props.data.name,
                    keyName: basisKeyName,
                };
                return obj;

                async function createFiltersData() {
                    for (let x of selectionRaw) {
                        let splitFilterByUnderscore = x.split("__");
                        let detail = await search.parseFilterMetaId(splitFilterByUnderscore[0]);
                        let filterData = {
                            raw: x,
                            metaIdNoDup: splitFilterByUnderscore[0],
                            details: detail,
                        };
                        if (detail.call_name === 'Skill') skills.push(filterData);
                        filtersData.push(filterData);
                    }
                }
            }
            res(allData);
        })
    }


    // Handle card click
    // Starts a search based on uri
    const handleCardOnClick = async () => {
        await storage.ss.setPair("currentURI", null);
        history.push(`/search?q=${uri}`);
    };

    return (
        <Card className="profile-grid-card">
            <CardActions className="card-actions-wrapper" disableSpacing>
                <IconButton
                    className="delete-button"
                    onClick={props.deleteFn}
                    size="small"
                >
                    <CloseIcon className="delete-button-icon" />
                </IconButton>
            </CardActions>

            <CardActionArea onClick={handleCardOnClick}>
                {data.hasFilters ? (
                    data.hasBasisKeyName ? (
                        <HasFilterAndNameCardDiv data={data} skillType={skillType} />
                    ) : (
                        <HasFilterCardNoBasisKeyNameDiv data={data} skillType={skillType} />
                    )
                ) : (
                    <NoFilterCardDiv data={data} />
                )}
            </CardActionArea>
        </Card>
    );
};

// no filters or default card cannot parse
const NoFilterCardDiv = (props) => {
    const [hasKeyName, setHasKeyName] = useState(false);

    useEffect(() => {
        try {
            let has = props.data.keyName.key && props.data.keyName.name;
            setHasKeyName(has);
        } catch (e) {
            setHasKeyName(false);
        }
    }, [props]);
    return (
        <div className="profile-details">
            <CardContent padding={0}>
                {hasKeyName ? (
                    <Typography>
                        Someone with the
                        <b className="card-key"> {props.data.keyName.key} </b>
                        <b>"{props.data.keyName.name}"</b>
                    </Typography>
                ) : (
                    <Typography>
                        <b>"{props.data.name}"</b>
                    </Typography>
                )}
            </CardContent>
        </div>
    );
};

// basis name (name, cell, email) and filters
const HasFilterAndNameCardDiv = (props) => {
    const classes = useStyles();
    const [anchorEl, setAnchorEl] = React.useState(null);

    const handlePopoverOpen = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handlePopoverClose = () => {
        setAnchorEl(null);
    };

    const open = Boolean(anchorEl);

    return (
        <div className="profile-details">
            <CardContent padding={0}>
                <Typography>
                    Someone with the
                    <b className="card-key"> {props.data.keyName.key} </b>
                    <b>"{props.data.keyName.name}"</b>
                </Typography>
                {props.data.filters.length > 0 ? (
                    <Typography variant="subtitle2" align={"left"}>
                        with the filter:{" "}
                        {props.data.filters[0].details.call_name} -{" "}
                        {props.data.filters[0].details.value_name}
                    </Typography>
                ) : null}
                {props.data.filters.length - 1 > 0 ? (
                    <>
                        <Typography
                            onMouseEnter={handlePopoverOpen}
                            onMouseLeave={handlePopoverClose}
                            variant="subtitle2"
                            align={"left"}
                        >
                            and {props.data.filters.length - 1} other filter(s)
                        </Typography>
                        <Popover
                            open={open}
                            className={classes.popover}
                            classes={{ paper: classes.paper }}
                            anchorEl={anchorEl}
                            anchorOrigin={{
                                vertical: "bottom",
                                horizontal: "left",
                            }}
                            transformOrigin={{
                                vertical: "top",
                                horizontal: "left",
                            }}
                            onClose={handlePopoverClose}
                        >
                            {props.data.filters
                                .slice(1, props.data.filters.length)
                                .map((filter) => (
                                    <Typography
                                        key={`${props.data.uid}-${filter.raw}`}
                                        variant="subtitle2"
                                    >
                                        {filter.details.call_name} -{" "}
                                        {filter.details.value_name}
                                    </Typography>
                                ))}
                            {
                                props.data.hasMoreThanOneSkill ? (
                                    <Typography variant="caption">
                                        {
                                            props.skillType === 'AND' ? (
                                                '(all of the skills listed)'
                                            ) : (
                                                '(any of the skills listed)'
                                            )
                                        }
                                    </Typography>
                                ) : (
                                    null
                                )
                            }
                        </Popover>
                    </>
                ) : null}
            </CardContent>
        </div>
    );
};

// only filters
const HasFilterCardNoBasisKeyNameDiv = (props) => {
    const classes = useStyles();
    const [anchorEl, setAnchorEl] = React.useState(null);

    const handlePopoverOpen = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handlePopoverClose = () => {
        setAnchorEl(null);
    };

    const open = Boolean(anchorEl);

    return (
        <div className="profile-details">
            <CardContent padding={0}>
                <Typography>Someone with the filter</Typography>
                {props.data.filters.length > 0 ? (
                    <Typography>
                        <b className="card-key">
                            {props.data.filters[0].details.call_name} -{" "}
                            {props.data.filters[0].details.value_name}
                        </b>
                    </Typography>
                ) : null}
                {props.data.filters.length - 1 > 0 ? (
                    <>
                        <Typography
                            onMouseEnter={handlePopoverOpen}
                            onMouseLeave={handlePopoverClose}
                            variant="subtitle2"
                            align={"left"}
                        >
                            and {props.data.filters.length - 1} other filter(s)
                        </Typography>
                        <Popover
                            open={open}
                            className={classes.popover}
                            classes={{ paper: classes.paper }}
                            anchorEl={anchorEl}
                            anchorOrigin={{
                                vertical: "bottom",
                                horizontal: "left",
                            }}
                            transformOrigin={{
                                vertical: "top",
                                horizontal: "left",
                            }}
                            onClose={handlePopoverClose}
                        >
                            {props.data.filters
                                .slice(1, props.data.filters.length)
                                .map((filter) => (
                                    <Typography
                                        key={`${props.data.uid}-${filter.raw}`}
                                        variant="subtitle2"
                                    >
                                        {filter.details.call_name} -{" "}
                                        {filter.details.value_name}
                                    </Typography>
                                ))
                            }
                            {
                                props.data.hasMoreThanOneSkill ? (
                                    <Typography variant="caption">
                                        {
                                            props.skillType === 'AND' ? (
                                                '(all of the skills listed)'
                                            ) : (
                                                '(any of the skills listed)'
                                            )
                                        }
                                    </Typography>
                                ) : (
                                    null
                                )
                            }
                        </Popover>
                    </>
                ) : null}
            </CardContent>
        </div>
    );
};


export default SearchCard;
