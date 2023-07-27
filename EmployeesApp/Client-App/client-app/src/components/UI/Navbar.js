import React from "react";
import styles from "./Navbar.module.css";

const Navbar = () => {
  return (
    <nav className={styles.navbar}>
      <div className={styles.logo}>Logo</div>
      <h2>Employees</h2>
    </nav>
  );
};

export default Navbar;
