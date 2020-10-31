import { User } from "oidc-client";
import React, { useEffect, useState } from "react";
import userManager from "../Authorization/userManager";

// API imports
import { getUsers, UserList } from "../Api/userlistApi";

// Material UI imports
import { Button, makeStyles, TextField } from '@material-ui/core';
import Icon from '@material-ui/core/Icon';
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import Autocomplete from '@material-ui/lab/Autocomplete';
import Chip from '@material-ui/core/Chip';


const useStyles = makeStyles(theme => ({
    oneliners: {
      position: "relative",
      width: '80%',
      margin: "auto",
      marginTop: "1em"
    },
    new_message:{
        position: "relative",
        width: '80%',
        margin: "auto",
        marginTop: "2em",
        whiteSpace: "pre-wrap"
    },
    buttons:
    {
        position: "relative",
        width: '80%',
        display:"flex",
        marginTop:"1em",
        margin: "auto",
        justifyContent:"space-between"
    },
    sendbutton:
    {
        margin: theme.spacing(1)
    }
    

  }));

interface NewMessageProps {
    user: User | null;
}
interface SelectingUsers{
    mail: string;
    favourite: boolean;
    selected: boolean;
}

const NewMessage = (props: NewMessageProps) => {
    const [error,setError] = useState<string>();
    const [users,setUsers] = useState<UserList[]>();
    const classes = useStyles();
    let to_users:SelectingUsers[] = [];
    let cc_users = [];

    useEffect(()=>{
        getUsers().then(result => {
            if(result.isError){
                setError(result.errorMessage);
            }
            else{
                setUsers(result.data);
            }
        })
    },[props.user]);
    let mails: SelectingUsers[];
    if(!users)
        mails = [];
    else
    {
        mails = users?.map((option) => { return { mail:option.mail,
             favourite:option.favourite, selected:false}});
    }
        

    return <>
        {error ? 
            <p>{error}</p>
            :
            <form>
            <div style={{display:"flex", flexDirection:"column", paddingTop:"2em"}}>
             <TextField type="text" label="From:"
                variant="filled"
             placeholder="From:" defaultValue="kenobi@jedi.com" required
              disabled
              className={classes.oneliners}></TextField>
            <Autocomplete
                className={classes.oneliners}
                id="to_field"
                multiple
                size="small"
                onChange={(object,values)=>
                    {
                        to_users=values;
                    }}
                options={mails.filter((x)=>x.selected===false)}
                getOptionLabel={(option) => option.mail}
                renderTags={(value, getTagProps) =>
                value.map((option, index) => (
                    <Chip
                    variant="outlined"
                    label={option.mail}
                    size="small"
                    {...getTagProps({ index })}
                    />
                ))
                }
                
                renderInput={(params) => (
                <TextField {...params} InputLabelProps={{required:true}} variant="filled" label="To:" />
                )}
            />
            <Autocomplete
                className={classes.oneliners}
                multiple
                inputMode="email"
                id="size-small-filled-multi"
                size="small"
                onChange={(object,values)=>
                    {
                        cc_users=values;
                    }}
                options={mails}
                getOptionLabel={(option) => option.mail}
                renderTags={(value, getTagProps) =>
                value.map((option, index) => (
                    <Chip
                    variant="outlined"
                    label={option.mail}
                    size="small"
                    {...getTagProps({ index })}
                    />
                ))
                }
                renderInput={(params) => (
                <TextField {...params} variant="filled" label="CC:" />
                )}
                
            />
              <TextField type="text" label="Subject" placeholder="Subject" variant="filled" required
              defaultValue="Hello There!"
              className={classes.oneliners}></TextField>
              <TextField type="text" label="Email text" variant="filled" rows="15"
              defaultValue={`Hello There!\n\n\nBest Regards,\nGeneral Kenobi`}
              className={classes.new_message} multiline></TextField>
              <div className={classes.buttons}>
              <Button variant="contained" color="default" className={classes.sendbutton}
                startIcon={<CloudUploadIcon />}>Upload</Button>
              <Button 
              type="submit"
              className={classes.sendbutton} variant="contained" color="primary" 
              onClick={(e) => {
                if(to_users.length===0)
                {
                    e.preventDefault();
                    return;
                }  
              }}
              endIcon={<Icon>send</Icon>}>Send </Button></div>
            </div>
            </form>
        }
    </>
    
}

export default NewMessage;