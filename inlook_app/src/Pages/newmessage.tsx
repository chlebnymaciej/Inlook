import { Button, DialogActions, DialogContent, DialogContentText, IconButton, List, ListItem, ListItemSecondaryAction, ListItemText, makeStyles, TextField, Typography } from '@material-ui/core';
import Chip from '@material-ui/core/Chip';
import Dialog from '@material-ui/core/Dialog/Dialog';
import DialogTitle from '@material-ui/core/DialogTitle/DialogTitle';
import FormHelperText from '@material-ui/core/FormHelperText/FormHelperText';
import Icon from '@material-ui/core/Icon';
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { User } from "oidc-client";
import React, { useEffect, useState } from "react";
import { useHistory } from 'react-router';
import { getGroups, GroupModel } from '../Api/groupsApi';
import { postMail } from '../Api/mailApi';
import { getUsers, UserModel } from "../Api/userApi";
import { TransitionProps } from "@material-ui/core/transitions";
import Slide from '@material-ui/core/Slide/Slide';
import { AttachmentInfo } from '../Api/attachmentsApi';
import * as attachmentsApi from "../Api/attachmentsApi";
import DeleteIcon from '@material-ui/icons/Delete';


const useStyles = makeStyles(theme => ({
    oneliners: {
        position: "relative",
        width: '80%',
        margin: "auto",
        marginTop: "1em"
    },
    inputGetters: {
        display: "flex",
        flexDirection: "row",
        width: '80%',
        margin: "auto",
        marginTop: "1em"
    },
    halfliners: {
        width: '50%',
        marginRight: "1em"
    },
    new_message: {
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
        display: "flex",
        marginTop: "1em",
        margin: "auto",
        justifyContent: "space-between"
    },
    sendbutton:
    {
        margin: theme.spacing(1),
    },
    uploadButton:
    {
        margin: theme.spacing(1),
    },
    formClass:
    {
        display: "flex",
        flexDirection: "column",
        paddingTop: "2em"
    },
    attachments:
    {
        position: "relative",
        width: '80%',
        display: "flex",
        marginTop: "1em",
        margin: "auto",
    }


}));

interface NewMessageProps {
    user: User | null;
}
interface ValidationErrors {
    to: string | null;
}
const Transition = React.forwardRef(function Transition(
    props: TransitionProps & { children?: React.ReactElement<any, any> },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="up" ref={ref} {...props} />;
});

const NewMessage = (props: NewMessageProps) => {

    const classes = useStyles();
    const history = useHistory();

    const [error, setError] = useState<string>();
    const [groups, setGroups] = useState<GroupModel[]>([]);
    const [helperText, setHelperText] = useState<string>();
    const [users, setUsers] = useState<UserModel[]>([]);
    const [toUsers, setToUsers] = useState<UserModel[]>([]);
    const [ccUsers, setCcUsers] = useState<UserModel[]>([]);
    const [bccUsers, setBccUsers] = useState<UserModel[]>([]);
    const [toGroups, setToGroups] = useState<GroupModel[]>([]);
    const [ccGroups, setCcGroups] = useState<GroupModel[]>([]);
    const [bccGroups, setBccGroups] = useState<GroupModel[]>([]);
    const [subject, setSubject] = useState<string>("Hello There!");
    const [mailValue, setMail] = useState<string>(`Hello There!\n\n\nBest Regards,\nGeneral Kenobi`);
    const [open, setOpen] = useState<boolean>(false);
    const hiddenFileInput = React.useRef<any>(null);
    const [attachments, setAttachments] = useState<AttachmentInfo[]>([]);

    const handleClose = () => {
        setOpen(false);
    };

    const sendMail = () => {
        setHelperText('');
        let bccuser = new Set<UserModel>();
        bccUsers.forEach(x => bccuser.add(x));
        bccGroups.forEach(x => {
            x.users?.forEach(y => bccuser.add(y));
        });
        let ccuser = new Set<UserModel>();
        ccUsers.forEach(x => {
            if (!(bccuser.has(x)))
                ccuser.add(x);
        });
        ccGroups.forEach(x => {
            x.users?.forEach(y => {
                if (!(bccuser.has(y)))
                    ccuser.add(y);
            });
        });

        let touser = new Set<UserModel>();
        toUsers.forEach(x => {
            if (!(bccuser.has(x) || ccuser.has(x)))
                touser.add(x);
        });
        toGroups.forEach(x => {
            x.users?.forEach(y => {
                if (!(bccuser.has(y) || ccuser.has(y)))
                    touser.add(y);
            });
        });


        postMail(
            {
                to: Array.from(touser).map(x => x.id),
                cc: Array.from(ccuser).map(x => x.id),
                bcc: Array.from(bccuser).map(x => x.id),
                subject: subject || null,
                text: mailValue || null,
                attachments: attachments.map(a => a.id),
            }).then(r => {
                if (r.isError) {
                    setError("Something went wrong");
                }
                else {
                    history.push('/')
                }
            });
        setOpen(false);
    };

    useEffect(() => {
        getUsers().then(result => {
            if (result.isError) {
                setError(result.errorMessage);
            }
            else {
                setUsers(result.data || []);
            }
        });
        getGroups().then(result => {
            if (result.isError) {
                setError(result.errorMessage);
            }
            else {
                setGroups(result.data || []);

            }
        });
    }, [props.user]);

    const submitHandled = (e: any) => {
        e.preventDefault();
        if (toUsers?.length === 0 && toGroups?.length === 0) {
            setHelperText('Field "To" or "To groups" cannot be empty');
            return;
        }
        if (subject.length < 1 || mailValue.length < 1) {
            setOpen(true);
            return;
        }
        sendMail();
    }

    const sendEmptyHandler = () => {
        if (subject.length < 1) {
            setSubject('(empty)');
        }
        if (mailValue.length < 1) {
            setMail('(empty)');
        }
        sendMail();
    }

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const files = event.target.files;
        if (!files) return;
        for (let i = 0; i < files.length; ++i) {
            attachmentsApi.uploadAttachment(files[i])
                .then(r => {
                    if (r.isError) {

                    }
                    else {
                        setAttachments(prev => r.data ? [...prev, r.data] : prev)
                    }
                });
            console.log(files[i]);
        }
    };

    const downloadAttachment = (id: string, fileName: string) => {
        attachmentsApi.getFile(id, fileName).catch(err => {
            setError(err);
        })
    }

    const deleteAttachment = (id: string) => {
        attachmentsApi.deleteAttachment(id).then(r => {
            if (r.isError) {

            }
            else {
                setAttachments(prev => prev.filter(att => att.id !== id));
            }
        });
    }

    return <>
        {error ?
            <p>{error}</p>
            :
            <div>
                <form onSubmit={submitHandled}>
                    <div className={classes.formClass}>
                        <TextField type="text" label="From:"
                            variant="filled"
                            placeholder="From:" value={props.user?.profile.email} defaultValue='test' required
                            disabled
                            className={classes.oneliners}></TextField>
                        <div className={classes.inputGetters}>
                            <Autocomplete
                                className={classes.halfliners}
                                id="to_field"
                                multiple
                                size="small"
                                onChange={(object, values) => setToUsers(values)}
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
                                    <TextField {...params} InputLabelProps={{ required: true }} variant="filled" label="To:" />
                                )}
                            />
                            <Autocomplete
                                className={classes.halfliners}
                                id="to_group_field"
                                multiple
                                size="small"
                                onChange={(object, values) => setToGroups(values)}
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
                                    <TextField {...params} InputLabelProps={{ required: true }} variant="filled" label="To groups:" />
                                )}
                            />
                        </div>
                        {helperText ? <FormHelperText className={classes.oneliners}>{helperText}</FormHelperText> : <></>}
                        <div className={classes.inputGetters}>
                            <Autocomplete
                                className={classes.halfliners}
                                multiple
                                size="small"
                                onChange={(object, values) => setCcUsers(values)}
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
                                className={classes.halfliners}
                                multiple
                                size="small"
                                onChange={(object, values) => setCcGroups(values)}
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
                        </div>
                        <div className={classes.inputGetters}>
                            <Autocomplete
                                className={classes.halfliners}
                                multiple
                                size="small"
                                onChange={(object, values) => setBccUsers(values)}
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
                                    <TextField {...params} variant="filled" label="BCC:" />
                                )}

                            />
                            <Autocomplete
                                className={classes.halfliners}
                                multiple
                                size="small"
                                onChange={(object, values) => setBccGroups(values)}
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
                                    <TextField {...params} variant="filled" label="BCC groups:" />
                                )}

                            />
                        </div>
                        <TextField
                            type="text"
                            label="Subject"
                            placeholder="Subject"
                            variant="filled"
                            defaultValue={subject}
                            className={classes.oneliners}
                            onChange={(event: any) => setSubject(event.target.value)}
                        ></TextField>
                        <TextField
                            type="text"
                            label="Email text"
                            variant="filled"
                            rows="15"
                            defaultValue={mailValue}
                            className={classes.new_message}
                            multiline
                            onChange={(event) => setMail(event.target.value)}
                        ></TextField>
                        <div className={classes.buttons}>
                            <input
                                type="file"
                                ref={hiddenFileInput}
                                onChange={handleFileChange}
                                style={{ display: 'none' }}
                                id="upload_input"
                                multiple
                            />
                            <label htmlFor="upload_input">
                                <Button variant="contained" color="default" className={classes.uploadButton} component="span"
                                    startIcon={<CloudUploadIcon />}>Upload</Button>
                            </label>
                            <Button
                                type="submit"
                                className={classes.sendbutton}
                                variant="contained"
                                color="primary"
                                endIcon={<Icon>send</Icon>}>
                                Send
                            </Button>

                        </div>
                        <div className={classes.attachments}>

                            <List dense>
                                {attachments.map(att =>
                                    <ListItem
                                        button
                                        onClick={() => downloadAttachment(att.id, att.clientFileName)}
                                    >
                                        <ListItemText
                                            primary={att.clientFileName}
                                        />
                                        <ListItemSecondaryAction>
                                            <IconButton edge="end" aria-label="delete" onClick={() => deleteAttachment(att.id)}>
                                                <DeleteIcon />
                                            </IconButton>
                                        </ListItemSecondaryAction>
                                    </ListItem>)
                                }
                            </List>
                        </div>

                    </div>
                </form>
                <Dialog
                    open={open}
                    TransitionComponent={Transition}
                    keepMounted
                    onClose={handleClose}
                    aria-labelledby="alert-dialog-slide-title"
                    aria-describedby="alert-dialog-slide-description"
                >
                    <DialogTitle id="alert-dialog-slide-title">{"Tell me want you want?"}</DialogTitle>
                    <DialogContent>
                        <DialogContentText id="alert-dialog-slide-description">
                            Do you really want to send message witohut subject or text?
                    </DialogContentText>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleClose} color="primary">
                            No
                    </Button>
                        <Button onClick={sendEmptyHandler} color="primary">
                            Yes
                    </Button>
                    </DialogActions>
                </Dialog>
            </div>
        }
    </>

}

export default NewMessage;