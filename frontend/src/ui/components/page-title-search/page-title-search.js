import React, { useEffect } from "react";
import "./page-title-search.css";
import ToggleView from "../../components/toggle-view";
import storage from "../../../services/storage";
import { useLocation } from 'react-router-dom';

const Title = (props) => {
  const location = useLocation();
  useEffect(async() => {
    document.getElementById('handleSort').disabled = true;
    let newTab = await storage.ss.getPair('newTab');
    if (newTab == 'true') {

      document.getElementById('tab-checkbox').checked = true;
    } else {
      document.getElementById('tab-checkbox').checked = false;
    }
    let select = await storage.ss.getPair('sort');
    if (select) {
      document.getElementById('handleSort').value = select;
    }

  }, [location.search])

  const handleTabs = async(event) => {
    if (event.target.checked) {
      await storage.ss.setPair('newTab', 'true');
    } else {
      await storage.ss.setPair('newTab', 'false');
    }
  }

  const handleSort = async(event) => {
    await storage.ss.setPair('sort', event.target.value);
  }

  return (
    <div className="page-title-search-wrapper">
      <div className="page-title-search-box">
        <div className="page-title-search">{props.data.title}</div>
        <div className="search-options">
          <label className="tab-switch">
            <p>Open results in new tab</p>
            <input id="tab-checkbox" type="checkbox" onChange={handleTabs}/>
            <span className="slider round"></span>
          </label>
          <div className="search-sort">
            <p>Sort By: </p>
            <select onChange={handleSort} id="handleSort">
              <option value="Default">Best Match</option>
              <option value="Alpha">Name</option>
            </select>
          </div>
        </div>
        <div className="page-title-search-toggle">
          <ToggleView />
        </div>
      </div>
    </div>
  );
};

export default Title;
