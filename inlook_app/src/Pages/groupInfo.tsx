import React, { useState } from "react";
import { makeStyles } from '@material-ui/core/styles';
import { deleteGroup, GroupModel } from "../Api/groupsApi";
import { User } from "oidc-client";
import { Button, DialogActions, DialogContent, DialogContentText, DialogTitle, List, ListItem } from "@material-ui/core";
import { UserModel } from "../Api/userApi";
import ListItemText from "@material-ui/core/ListItemText/ListItemText";
import Dialog from "@material-ui/core/Dialog/Dialog";
import { TransitionProps } from "@material-ui/core/transitions";
import Slide from "@material-ui/core/Slide/Slide";
import { useHistory } from "react-router";

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

interface GroupInfoProps {
  user: User;
  group: GroupModel;
}
const Transition = React.forwardRef(function Transition(
  props: TransitionProps & { children?: React.ReactElement<any, any> },
  ref: React.Ref<unknown>,
) {
  return <Slide direction="up" ref={ref} {...props} />;
});

const GroupInfo = (props: any) => {

  const group: GroupInfoProps =
  (props.location && props.location.state) || {};
  const classes = useStyles();
  const history = useHistory();

  const [open, setOpen] = useState<boolean>(false);
  const handleClose = () => {
    setOpen(false);
  };
  const deleteClicked = ()=>
  {
    deleteGroup(group.group.id);
    setOpen(false);
    history.push('/groups');
  };

  return (
    <div className={classes.root}>
        <div className={classes.firstLine}>
          <h3>{group.group.name}</h3>
          <div>
            <Button 
            className={classes.buttonEdit}
            onClick={
              (e)=> history.push({
                  pathname:'/groupEdit',
                  state: group
              })}
            >Edit</Button>
            <Button className={classes.buttonDelete} onClick={()=>setOpen(true)}>Delete</Button>
            <Dialog
              open={open}
              TransitionComponent={Transition}
              keepMounted
              onClose={handleClose}
              aria-labelledby="alert-dialog-slide-title"
              aria-describedby="alert-dialog-slide-description"
            >
              <DialogTitle id="alert-dialog-slide-title">{"Delete this group?"}</DialogTitle>
              <DialogContent>
                <DialogContentText id="alert-dialog-slide-description">
                  Do you really want you delete this group?
                </DialogContentText>
              </DialogContent>
              <DialogActions>
                <Button onClick={handleClose} color="primary">
                  No
                </Button>
                <Button onClick={deleteClicked} color="primary">
                  Yes
                </Button>
              </DialogActions>
            </Dialog>
          </div>
        </div>
        <List>
            { group.group.users?.map((x: UserModel)=>{
              return (
                <ListItem key={x.id}>
                  <ListItemText primary={x.name+" "+x.email}></ListItemText> 
                </ListItem>
              );
            })}
          </List>
    </div>
  );
};

export default GroupInfo;