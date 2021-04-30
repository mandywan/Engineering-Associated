/*
    Filter Modal Component
 */

import React, { useEffect, useState, useReducer } from 'react';
import Modal from '@material-ui/core/Modal';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Checkbox from '@material-ui/core/Checkbox';
import FormGroup from '@material-ui/core/FormGroup';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import filters from "../../../services/filters";
import Button from '@material-ui/core/Button';
import storage from '../../../services/storage';
import { useHistory, useLocation } from "react-router-dom";
import * as qs from 'query-string';
import EventEmitter from '../../hooks/event-manager';

function TabPanel(props) {
    const {children, value, index} = props;
  
    return (
      <div
        role="tabpanel"
        hidden={value !== index}
        id={`vertical-tabpanel-${index}`}
        className="vertical-tabpanel"
        aria-labelledby={`vertical-tab-${index}`}
      >
        {value === index && (
          <section className="tabpanel_padding">
            {children}
          </section>
        )}
      </div>
    );
  }
  
  function a11yProps(index) {
    return {
      id: `vertical-tab-${index}`,
      'aria-controls': `vertical-tabpanel-${index}`,
      key: index
    };
  }

 let selection = [];
 let selectionRaw = [];
 const Filter_modal = () => {
    let history = useHistory();
    const location = useLocation();
    const [open, setOpen] = React.useState(false);
    const [value, setTabValue] = React.useState(0);
    const [filterDocs, setFilterDocs] = React.useState(null);
    let [selectionData, setSelectionData] = useState(selectionRaw);
    let [ORSkill, setORSkill] = useState(false);
    let [ANDSkill, setANDSkill] = useState(true);
    let idx = 0;
    
    useEffect(() => {
        document.getElementById("filter_open_button").onclick = async() => {
            setOpen(true);
            let query = qs.parse(location.search);
            if (query.q) {
              let data = JSON.parse(decodeURIComponent(query.q));
              if (data.Skill) {
                if (data.Skill.type.toLowerCase() == 'or') {
                  setORSkill(true);
                  setANDSkill(false);
                  await storage.ss.setPair('skillType', 'OR');
                } else {
                  setORSkill(false);
                  setANDSkill(true);
                  await storage.ss.setPair('skillType', 'AND');
                }
              } else {
                await storage.ss.setPair('skillType', 'AND');
              }
              if(data.meta) {
                selectionRaw = data.meta;
                setSelectionData(selectionRaw);
                selection = [];
                for (let x of selectionRaw) {
                  let item = x.split("__");
                  selection = selection.concat(item);
                }
              }
            }

            //selection = [];
            // make all the filters to be displayed in the modal
            let filterManifest = await filters.getFilterList("Selection");

            for (let i = 0; (i < filterManifest.length); i++) {
              let item = filterManifest[i];
              let supplimentaryData = [];
              let buildManifest = [];
              for (let metaitem of item.metadata) {
                let val = `${metaitem.call_name}_${metaitem.value_name}`;
                if(!supplimentaryData.includes(val)) {
                  supplimentaryData.push(val);
                  buildManifest.push(metaitem);
                } else {
                  let meta_id = buildManifest[supplimentaryData.indexOf(val)].meta_id;
                  meta_id = meta_id + `__${metaitem.meta_id}`;
                  buildManifest[supplimentaryData.indexOf(val)].meta_id = meta_id;
                }

                filterManifest[i].metadata = buildManifest;
              }
            }

            for (let i = 0; (i < filterManifest.length); i++) {
              filterManifest[i].metadata = filterManifest[i].metadata.sort(function(a, b) {
                var val1 = a.value_name.toLowerCase();
                var val2 = b.value_name.toLowerCase();
                if (val1 < val2) {
                  return -1;
                }
                if (val1 > val2) {
                  return 1;
                }
                return 0;
              });
              
            }
            //let temp = filterManifest;
            //let splDoc = filterManifest.slice(4, 6);
            //filterManifest = temp.slice(0, 4);
            
            if (filterManifest[4].call_name == "Category") {
              filterManifest[4].call_name = filterManifest[5].call_name;
              filterManifest[4].display_name = filterManifest[5].display_name;
              filterManifest[4]._uuid = filterManifest[5]._uuid;
              filterManifest[4].spl = [];

              for (let x = 0; x < filterManifest[4].metadata.length; x++) {

                let spl = {};
                spl.title = filterManifest[4].metadata[x].value_name;
                spl.skills = [];
                for (let skill of filterManifest[5].metadata) {
                  if (filterManifest[4].metadata[x].value_id[0] == skill.value_id[0]) {
                    spl.skills.push(skill);
                  }
                }

                filterManifest[4].spl.push(spl);

              }

              delete filterManifest[5];
            }

            setFilterDocs(filterManifest);
        }

      }, [history.location.key]);

    const handleClose = () => {
        setOpen(false);
    };
    
    const handleFilterSelection = (event) => {
        
        // event.target.value is the meta id for the filter modal
        if (event.target.checked) {
            selectionRaw.push(event.target.value);
            let userSel = event.target.value.split("__");
            selection = selection.concat(userSel);
        } else if (selectionRaw.indexOf(event.target.value) > -1) {
            selectionRaw.splice(selectionRaw.indexOf(event.target.value),1);
            let userSel = event.target.value.split("__");
            for (let x of userSel) {
              selection.splice(selection.indexOf(x),1);
            }
        }

        selection = [...new Set(selection)];
        selectionRaw = [...new Set(selectionRaw)];
        setSelectionData(selectionRaw);
    }

    const handleSkill = async(val) => {
      await storage.ss.setPair('skillType', val);
    }

    const handleSubmit = async() => {
        let attach = await storage.ss.getPair('search_key');
        attach = JSON.parse(attach);

        let skillType = await storage.ss.getPair('skillType');
        let qstr = await filters.getQS(selection, attach, selectionRaw, skillType);
        await storage.ss.setPair('currentURI', null);
        let basis = await storage.ss.getPair('basisName');
        if (basis == "(Blank Search)" && (selection.length > 0)) {
          let name = await storage.db.searchDocument('metadata', {meta_id: selection[0]});
          await storage.ss.setPair('basisName', name[0].value_name);
          //return;
        }
        setOpen(false);
        history.push(`/search?q=${qstr}`);
    }

    const getCheckboxState = (meta_id) => {
      if (selection.includes(meta_id)) {
        return true;
      } else {
        return false;
      }
    } 

    const handleChangeTabs = (event, newValue) => {
      setTabValue(newValue);
    };
     
    return (
        <div>
          <Modal
            open={open}
            onClose={handleClose}
            aria-labelledby="simple-modal-title"
            aria-describedby="simple-modal-description"
            id="filter_modal"
          >
            <section className="modal_body">
            <h3>Advanced Search</h3>
            <div className="tabpanel_wrapper">
            <Tabs
              orientation="vertical"
              variant="scrollable"
              value={value}
              onChange={handleChangeTabs}
            >
              {filterDocs !== null ? (filterDocs.map((filterDoc) => (
                <Tab label={filterDoc.display_name} {...a11yProps(filterDoc._uuid)} />
              ))) : (null)}
            </Tabs>

            {filterDocs !== null ? (filterDocs.map((filterDoc) => (
              <TabPanel key={'panel-'+idx} value={value} index={idx++}>
                { filterDoc.call_name.toLowerCase() == "skill" ? ( 
                  <div className="filter-model-selbox">
                    <center>
                      <span> Filtering Mode: </span>
                      <select name="skillType" id="skillType" onChange={() => {handleSkill(document.getElementById('skillType').value)}}>
                        <option value="OR" selected={ORSkill}>Any of these</option>
                        <option value="AND" selected={ANDSkill}>All of these</option>
                      </select>
                    </center>
                  </div>
                ) : (null)}

              {filterDoc.call_name.toLowerCase() != "skill" ? filterDoc.metadata.map((e) => (
                
                <FormGroup row key={`panelrow-${idx}-${e.meta_id}`}>
                    <FormControlLabel
                        control={<Checkbox value={e.meta_id} name="filter_checkbox" checked={selectionData.includes(e.meta_id)} onChange={handleFilterSelection} color="primary"/>}
                        label={e.value_name}
                    />
                  </FormGroup>
                        
              )): (null)}

              {filterDoc.call_name.toLowerCase() == "skill" ? filterDoc.spl.map((e) => (
                <div className="skill-category-wrapper" key={`panelrow-${idx}-${filterDoc.spl.indexOf(e)}plus`}>
                  <section className="skill-category-title"><p>{e.title}</p></section>

                    {e.skills.map((s) => (
                      <FormGroup row key={`panelrow-${idx}-${s.meta_id}`}>
                        <FormControlLabel
                            control={<Checkbox value={s.meta_id} name="filter_checkbox" checked={selectionData.includes(s.meta_id)} onChange={handleFilterSelection} color="primary"/>}
                            label={s.value_name}
                        />
                      </FormGroup>
                    ))}
                </div>
                        
              )): (null)}

              </TabPanel>
                
            ))) : (null)}
            
            
        
          </div>
            <Button variant="contained" id="submit_filters" onClick={handleSubmit} color="primary">
                Apply Filters →
            </Button>
            </section>
          </Modal>
        </div>
      );
 }

 export default Filter_modal;