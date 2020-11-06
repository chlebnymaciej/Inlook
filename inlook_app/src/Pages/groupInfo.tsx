import React from "react";
import { makeStyles } from '@material-ui/core/styles';
import { GroupModel } from "../Api/groupsApi";
import { User } from "oidc-client";

const useStyles = makeStyles({
   root:{
       display:"flex",
       flexDirection:"column",
       justifyContent:"center",
       width:"80%",
       margin:"1em auto auto auto",
       color:"black"
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
        {group.name}
    </div>
  );
};

export default GroupInfo;