import React, { useState, useEffect, useReducer } from "react";
import "./sub-header.css";
import FilterIcon from "../../../../assets/filter-icon.svg";
import CloseIcon from "../../../../assets/close-icon.svg";
import { Button, Divider, Typography, Grid } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import FilterChip from "../../filter-chip";
import filtersService from "../../../../services/filters";
import search from "../../../../services/search";
import storage from "../../../../services/storage";
import EventEmitter from "../../../hooks/event-manager";
import { useHistory, useLocation } from "react-router-dom";

const useStyles = makeStyles((theme) => ({
    button: {},
    buttonText: {
        paddingLeft: 10,
        textTransform: "none",
        fontWeight: 600,
        fontSize: 18,
    },
    divider: {
        height: 28,
        marginTop: 4,
        marginBottom: 4,
        marginLeft: 10,
        marginRight: 10,
    },
    chip: {
        fontSize: 14,
        color: "#ffffff",
        margin: 4,
        padding: 4,
    },
}));

const Subheader = (props) => {
    const classes = useStyles();
    let history = useHistory();
    const location = useLocation();

    const [selectedFilters, setSelectedFilters] = useState([]);
    const [selectedMetaIds, setSelectedMetaIds] = useState([]);
    const [selectionsRaw, setSelectionsRaw] = useState(props.selectionsRaw);
    const [filters, setFilters] = useState([]);

    // useEffect(() => {
    //     const parse = async () => {
    //         let selectionRaw = [...props.selectionsRaw];
    //         let metaIds = [];

    //         // only push one meta id
    //         // check if it is has a double underscore, which means duplicate group
    //         for (let x of selectionRaw) {
    //             let splitFilterByUnderscore = x.split("__");
    //             let detail = await parseFilterMetaId(splitFilterByUnderscore[0]);
    //             let obj = { raw: x, metaIdNoDup: splitFilterByUnderscore[0], details: detail };
    //             metaIds.push(obj);
    //         }
    //         setFilters(metaIds);
    //     }
    //     parse();

    // }, [props]);

    const handleChipDelete = async (item) => {
        // let filter_to_delete;
        let newFilters;
        async function setNewData() {
            // make new selectedFilters
            newFilters = filters.filter((d) => d.raw !== item.raw);

            // get the filter to delete
            // filter_to_delete = filters.find((d) => d.raw === item.raw);

            // update state
            // setFilters(newFilters);
        }
        await setNewData();
        
        // let event_filter_meta_id = filter_to_delete.raw;
        let event_selection = Array.from(newFilters, (d) => d.raw);
        let sel = [];
        for (let x of event_selection) {
            let item = x.split("__");
            sel = sel.concat(item);
          }

        // emit deleteChip event to filter modal
        let attach = await storage.ss.getPair('search_key');
        attach = JSON.parse(attach);
        let skillType = await storage.ss.getPair('skillType');
        if (!skillType) {
            skillType = 'OR';
        }
        let qstr = await filtersService.getQS(sel, attach, event_selection, skillType);
        await storage.ss.setPair('currentURI', null);
        history.push(`/search?q=${qstr}`);

    };

    // parse one filter meta id to get all details
    const parseFilterMetaId = async (metaId) => {
        let metaIdParsed = await search.parseFilterMetaId(metaId);
        return metaIdParsed;
    };

    EventEmitter.addListener("updateChips", async (data) => {
        let selectionRaw;
        let metaIds;
        const parse = async () => {
            selectionRaw = [...data];
            metaIds = [];

            // only push one meta id
            // check if it is has a double underscore, which means duplicate group
            for (let x of selectionRaw) {
                let splitFilterByUnderscore = x.split("__");
                let detail = await parseFilterMetaId(splitFilterByUnderscore[0]);
                let obj = { raw: x, metaIdNoDup: splitFilterByUnderscore[0], details: detail };
                metaIds.push(obj);
            }
            setFilters(metaIds);
        }
        await parse();
    })

    return (
        <div>
            <div className="subheader-wrapper">
                <Button className={classes.button} id="filter_open_button">
                    <img width="24" height="24" src={FilterIcon}></img>
                    <Typography className={classes.buttonText}>
                        Filter
                    </Typography>
                </Button>
                <Divider
                    className={classes.divider}
                    color="primary"
                    orientation="vertical"
                />
                <div className="filter-chips">
                    {filters.map((d) => (
                        <FilterChip
                            key={d.metaIdNoDup}
                            data={d}
                            handleDelete={() => handleChipDelete(d)}
                        />
                    ))}
                </div>
            </div>
        </div>
    );
};

export default Subheader;
