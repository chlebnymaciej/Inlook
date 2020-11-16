import { Button, makeStyles, TextField } from '@material-ui/core';
import Chip from '@material-ui/core/Chip';
import FormHelperText from '@material-ui/core/FormHelperText/FormHelperText';
import Icon from '@material-ui/core/Icon';
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { User } from "oidc-client";
import React, { useEffect, useState } from "react";
import { getGroups, GroupModel } from '../Api/groupsApi';
import { postMail } from '../Api/sendMailApi';
import { getUserMail, getUsers, UserModel } from "../Api/userApi";


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
    const [groups, setGroups] = useState<GroupModel[]>([]);
    const [helperText, setHelperText] = useState<string>();
    const [users,setUsers] = useState<UserModel[]>([]);
    const [toUsers, setToUsers] = useState<UserModel[]>([]);
    const [ccUsers, setCcUsers] = useState<UserModel[]>([]);
    const [toGroups, setToGroups] = useState<GroupModel[]>([]);
    const [ccGroups, setCcGroups] = useState<GroupModel[]>([]);
    const [userMail, setUserMail] = useState<string>();
    const [subject, setSubject] = useState<string>("Hello There!");
    const [mailValue, setMail] = useState<string>(`Hello There!\n\n\nBest Regards,\nGeneral Kenobi`);
    
    const classes = useStyles();

    useEffect(()=>{
        getUsers().then(result => {
            if(result.isError){
                setError(result.errorMessage);
            }
            else{
                setUsers(result.data || []);

            }
        });
        getGroups().then(result => {
            if(result.isError){
                setError(result.errorMessage);
            }
            else{
                setGroups(result.data || []);

            }
        });
        getUserMail().then(result => {
            if(result.isError)
                setError(result.errorMessage);
            else
            {
                console.log(result.data);
                setUserMail(result.data);
            }
        });
    },[props.user]);
        
    const submitHandled = (e:any) => {
        e.preventDefault();
        if(toUsers?.length===0 && toGroups?.length===0)
        {
            setHelperText('Field "To" or "To groups" cannot be empty');
            return;
        }
        setHelperText('');
        let touser = new Set<UserModel>();
        toUsers.forEach(x => touser.add(x));
        toGroups.forEach(x=>{
            x.users?.forEach(y => touser.add(y));
        });
        let ccuser = new Set<UserModel>();
        ccUsers.forEach(x => ccuser.add(x));
        ccGroups.forEach(x=>{
            x.users?.forEach(y => ccuser.add(y));
        });

        postMail(
        {
        to:Array.from(touser).map(x => x.id),
        cc:Array.from(ccuser).map(x => x.id),
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
             placeholder="From:" value={userMail} defaultValue='test' required
             disabled
             className={classes.oneliners}></TextField>
            <Autocomplete
                className={classes.oneliners}
                id="to_field"
                multiple
                size="small"
                onChange={(object,values)=>setToUsers(values)}
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
            <Autocomplete
                className={classes.oneliners}
                id="to_group_field"
                multiple
                size="small"
                onChange={(object,values)=> setToGroups(values) }
                options={groups}
                getOptionLabel={(option) => option?.name}
                renderTags={(value, getTagProps) =>
                value.map((option, index) => (
                    <Chip
                    variant="outlined"
                    label={option.name}
                    size="small"
                    {...getTagProps({ index })}
                    />
                ))
                }
                renderInput={(params) => (
                <TextField {...params} InputLabelProps={{required:true}} variant="filled" label="To groups:" />
                )}
            />
           {helperText ? <FormHelperText className={classes.oneliners}>{helperText}</FormHelperText>:<></> }
            <Autocomplete
                className={classes.oneliners}
                multiple
                size="small"
                onChange={(object,values)=> setCcUsers(values)}
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
            <Autocomplete
                className={classes.oneliners}
                multiple
                size="small"
                onChange={(object,values)=> setCcGroups(values)}
                options={groups}
                getOptionLabel={(option) => option?.name}
                renderTags={(value, getTagProps) =>
                value.map((option, index) => (
                    <Chip
                    variant="outlined"
                    label={option.name}
                    size="small"
                    {...getTagProps({ index })}
                    />
                ))
                }
                renderInput={(params) => (
                <TextField {...params} variant="filled" label="CC groups:" />
                )}
                
            />
            <TextField 
                type="text"
                label="Subject"
                placeholder="Subject"
                variant="filled"
                defaultValue={subject}
                className={classes.oneliners}
                onChange={handleSubjectChanged}
            ></TextField>
            <TextField 
                type="text"
                label="Email text" 
                variant="filled"
                rows="15"
                defaultValue={mailValue}
                className={classes.new_message} 
                multiline
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