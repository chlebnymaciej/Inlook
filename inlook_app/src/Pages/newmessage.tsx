import { Button, makeStyles, TextField } from '@material-ui/core';
import Chip from '@material-ui/core/Chip';
import FormHelperText from '@material-ui/core/FormHelperText/FormHelperText';
import Icon from '@material-ui/core/Icon';
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { User } from "oidc-client";
import React, { useEffect, useState } from "react";
import { postMail } from '../Api/sendMailApi';
import { getUsers, UserModel } from "../Api/userApi";


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
    },
    formClass:
    {
        display:"flex",
        flexDirection:"column",
        paddingTop:"2em"
    }
    

  }));

interface NewMessageProps {
    user: User | null;
}
interface ValidationErrors {
    to: string | null;
}

const NewMessage  = (props: NewMessageProps) => {
    const [error,setError] = useState<string>();
    const [users,setUsers] = useState<UserModel[]>([]);
    const [toUsers, setToUsers] = useState<UserModel[]>([]);
    const [ccUsers, setCcUsers] = useState<UserModel[]>([]);
    const [subject, setSubject] = useState<string>("Hello There!");
    const [mailValue, setMail] = useState<string>(`Hello There!\n\n\nBest Regards,\nGeneral Kenobi`);

    const [helperText, setHelperText] = useState<string>();
    
    const classes = useStyles();

    useEffect(()=>{
        getUsers().then(result => {
            if(result.isError){
                setError(result.errorMessage);
            }
            else{
                setUsers(result.data || []);
                console.log(result.data);
            }
        })
    },[props.user]);
        
    const submitHandled = (e:any) => {
        e.preventDefault();
        if(toUsers?.length===0)
        {
            setHelperText('Field "To" cannot be empty');
            return;
        }
        setHelperText('');
        postMail(
        {
        to:toUsers.map(x => x.id),
        cc:toUsers.map(x => x.id),
        subject:subject ||  null,
        text:mailValue || null
        });
      }

    const handleSubjectChanged = (event: any) =>
    {
        setSubject(event.target.value);
    }
    return <>
        {error ? 
            <p>{error}</p>
            :
            <form onSubmit={submitHandled}>
            <div className={classes.formClass}>
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
                        setToUsers(values);
                    }}
                options={users}
                getOptionLabel={(option) => option?.email}
                renderTags={(value, getTagProps) =>
                value.map((option, index) => (
                    <Chip
                    variant="outlined"
                    label={option.email}
                    size="small"
                    {...getTagProps({ index })}
                    />
                ))
                }
                
                renderInput={(params) => (
                <TextField {...params} InputLabelProps={{required:true}} variant="filled" label="To:" />
                )}
            />
           {helperText ? <FormHelperText className={classes.oneliners}>{helperText}</FormHelperText>:<></> }
            <Autocomplete
                className={classes.oneliners}
                multiple
                inputMode="email"
                id="size-small-filled-multi"
                size="small"
                onChange={(object,values)=>
                    {
                        setCcUsers(values);
                    }}
                options={users}
                getOptionLabel={(option) => option.email}
                renderTags={(value, getTagProps) =>
                value.map((option, index) => (
                    <Chip
                    variant="outlined"
                    label={option.email}
                    size="small"
                    {...getTagProps({ index })}
                    />
                ))
                }
                renderInput={(params) => (
                <TextField {...params} variant="filled" label="CC:" />
                )}
                
            />
              <TextField type="text" label="Subject" placeholder="Subject" variant="filled"
              defaultValue="Hello There!"
              className={classes.oneliners}
              onChange={handleSubjectChanged}
              ></TextField>
              <TextField type="text" label="Email text" variant="filled" rows="15"
              defaultValue={`Hello There!\n\n\nBest Regards,\nGeneral Kenobi`}
              className={classes.new_message} multiline
              onChange={(event)=> setMail(event.target.value)}
              ></TextField>
              <div className={classes.buttons}>
              <Button variant="contained" disabled color="default" className={classes.sendbutton}
                startIcon={<CloudUploadIcon />}>Upload</Button>
              <Button 
              type="submit"
              className={classes.sendbutton}
              variant="contained"
              color="primary" 
              endIcon={<Icon>send</Icon>}>Send </Button></div>
            </div>
            </form>
        }
    </>
    
}

export default NewMessage;