import React from "react";
import "./header.css";
import Subheader from "./sub-header";
import FilterModal from "../filter_modal/filter_modal"

const Header = (props) => {

  return (
    <section className="header-wrapper">
      <div className="header-under-box-wrapper">
        <div className="header-under-box">
        <Subheader selectionsRaw={props.data} />
        <FilterModal metasRaw={props.data} />
        </div>
      </div>
    </section>
  );
};

export default Header;
