import React from "react";
import { makeStyles } from '@material-ui/core/styles';
import { GroupModel } from "../Api/groupsApi";
import { User } from "oidc-client";
import { Button } from "@material-ui/core";

const useStyles = makeStyles({
   root:{
       display:"flex",
       flexDirection:"column",
       justifyContent:"center",
       width:"80%",
       margin:"1em auto auto auto",
       color:"black"
   },
   firstLine:{
    display:"flex",
    justifyContent:"space-between",
   },
   buttonDelete:{
    background:"#EC4849",
    color:"white",
    width:"5em",
    margin:"0.5em",
    '&:hover':{
      backgroundColor:"#DAE0E2",
      color:"black"
  }
  },
  buttonEdit:{
    background:"#F3B431",
    color:"white",
    width:"5em",
    margin:"0.5em",
    '&:hover':{
      backgroundColor:"#DAE0E2",
      color:"black"
  }
  }
});

interface GroupsProps {
    user: User | null;
}
const GroupInfo = (props: any) => {
    const group: GroupModel =
    (props.location && props.location.state) || {};
    const classes = useStyles();

  return (
    <div className={classes.root}>
        <div className={classes.firstLine}>
          <h3>{group.name}</h3>
          <div>
            <Button className={classes.buttonEdit}>Edit</Button>
            <Button className={classes.buttonDelete}>Delete</Button>
          </div>
        </div>
    </div>
  );
};

export default GroupInfo;