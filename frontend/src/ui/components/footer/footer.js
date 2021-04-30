import React from 'react';
import './footer.css';

const Footer = () => {

  let year = new Date().getFullYear();
  return (
    <footer id="site-footer">
      <center>
        © {year} Associated Engineering. All Rights Reserved. <br/>
        <small>Treat all data as confidential.</small>
      </center>
    </footer>
  );


}

export default Footer;