import { User } from "oidc-client";
import React, { useEffect, useState } from "react";
import { getWeather, WeatherForecast } from "../Api/weatherApi";
import userManager from "../Authorization/userManager";
import { Button, makeStyles, TextField } from '@material-ui/core';
import Icon from '@material-ui/core/Icon';
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import Autocomplete from '@material-ui/lab/Autocomplete';
import Chip from '@material-ui/core/Chip';
import { getUsers, UserList } from "../Api/userlistApi";

const useStyles = makeStyles(theme => ({
    oneliners: {
      position: "relative",
      width: '80%',
      height: "3em",
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

const NewMessage = (props: NewMessageProps) => {
    const [error,setError] = useState<string>();
    const [users,setUsers] = useState<UserList[]>();
    const classes = useStyles();

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
    let mails: string[];
    if(!users)
        mails = [];
    else
        mails = users?.map((option) => option.mail);

    return <>
        {error ? 
            <p>{error}</p>
            :
            <form>
            <div style={{display:"flex", flexDirection:"column", paddingTop:"2em"}}>
             <TextField type="text" placeholder="From:" defaultValue="kenobi@jedi.com" required
              disabled
              className={classes.oneliners}></TextField>
            <Autocomplete
                    className={classes.oneliners}
                    multiple
                    id="tags-filled"
                    options={mails}
                    renderTags={(value: string[], getTagProps) =>
                    value.map((option: string, index: number) => (
                        <Chip variant="outlined" label={option} {...getTagProps({ index })} />
                    ))
                    }
                    renderInput={(params) => (
                    <TextField {...params} variant="filled" placeholder="To:" />
                    )}
                />
              <TextField type="text" placeholder="CC:"
              className={classes.oneliners}></TextField>
              <TextField type="text" placeholder="Subject" defaultValue="Hello There!"
              className={classes.oneliners}></TextField>
              <TextField type="text" placeholder="Email text" rows="15"
              defaultValue={`Hello There!\n\n\nBest Regards,\nGeneral Kenobi`}
              className={classes.new_message} multiline></TextField>
              <div className={classes.buttons}>
              <Button variant="contained" color="default" className={classes.sendbutton}
                startIcon={<CloudUploadIcon />}>Upload</Button>
              <Button type="submit" className={classes.sendbutton} variant="contained" color="primary" endIcon={<Icon>send</Icon>}>Send </Button>              </div>
            </div>
            </form>
        }
    </>
    
}

export default NewMessage;