import { makeStyles } from "@material-ui/core";
import React from "react";
import inlookLogo from "./../Resources/logo_duze.png";
const useStyles = makeStyles(theme => ({
    header_my:{
        margin:"auto",
        display:"flex",
        flexDirection:"column",
        alignItems:"center",
        justifyContent:"center"
    }
  }));

const Home = () => {
    const classes = useStyles();

    return <div className={classes.header_my} >
        <img src={inlookLogo} alt="Inlook logo"/>
        <h1>Inlook!</h1>
        <h3>We make Outlook look retarded</h3>
    </div>
    
}

export default Home;