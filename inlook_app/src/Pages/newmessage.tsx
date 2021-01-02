import { Button, DialogActions, DialogContent, DialogContentText, IconButton, List, ListItem, ListItemSecondaryAction, ListItemText, makeStyles, TextField } from '@material-ui/core';
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
import { useSnackbar } from 'notistack';


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
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();
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
    const [attachments, setAttachments] = useState<AttachmentInfo[]>([]);

    const handleClose = () => {
        setOpen(false);
    };

    const sendMail = () => {
        setHelperText('');
        let bccuser = new Set<string>();
        bccUsers.forEach(x => bccuser.add(x.id));
        bccGroups.forEach(x => {
            x.users?.forEach(y => bccuser.add(y.id));
        });
        let ccuser = new Set<string>();
        ccUsers.forEach(x => {
            if (!(bccuser.has(x.id)))
                ccuser.add(x.id);
        });
        ccGroups.forEach(x => {
            x.users?.forEach(y => {
                if (!(bccuser.has(y.id)))
                    ccuser.add(y.id);
            });
        });

        let touser = new Set<string>();
        toUsers.forEach(x => {
            if (!(bccuser.has(x.id) || ccuser.has(x.id)))
                touser.add(x.id);
        });
        toGroups.forEach(x => {
            x.users?.forEach(y => {
                if (!(bccuser.has(y.id) || ccuser.has(y.id)))
                    touser.add(y.id);
            });
        });


        postMail(
            {
                to: Array.from(touser),
                cc: Array.from(ccuser),
                bcc: Array.from(bccuser),
                subject: subject || null,
                text: mailValue || null,
                attachments: attachments.map(a => a.id),
            }).then(r => {
                if (r.isError) {
                    enqueueSnackbar("Something went wrong", { variant: "error" });
                }
                else {
                    enqueueSnackbar("Email send", { variant: "success" });
                    history.push('/inbox');
                }
            });
        setOpen(false);
    };

    useEffect(() => {
        getUsers().then(result => {
            if (result.isError) {
                enqueueSnackbar("Could not load users", { variant: "error" });
            }
            else {
                setUsers(result.data || []);
            }
        });
        getGroups().then(result => {
            if (result.isError) {
                enqueueSnackbar("Could not load groups", { variant: "error" });
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
                        enqueueSnackbar("Could not upload attachment", { variant: "error" });
                    }
                    else {
                        setAttachments(prev => r.data ? [...prev, r.data] : prev)
                    }
                });
            console.log(files[i]);
        }
    };

    const downloadAttachment = (id: string, fileName: string) => {
        attachmentsApi.getFile(id, fileName).then(r => {
            if (r.isError) {
                enqueueSnackbar("Could not download attachment", { variant: "error" });
            }
        });
    }

    const deleteAttachment = (id: string) => {
        attachmentsApi.deleteAttachment(id).then(r => {
            if (r.isError) {
                enqueueSnackbar("Could not delete attachment", { variant: "error" });
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
                                id="cc_users_field"
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
                                id="cc_groups_field"
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
                                id="bcc_user_field"
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
                                id="bcc_group_field"
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
                            id='subject_field'
                            defaultValue={subject}
                            className={classes.oneliners}
                            onChange={(event: any) => setSubject(event.target.value)}
                        ></TextField>
                        <TextField
                            type="text"
                            label="Email text"
                            variant="filled"
                            id="email_text_field"
                            rows="15"
                            defaultValue={mailValue}
                            className={classes.new_message}
                            multiline
                            onChange={(event) => setMail(event.target.value)}
                        ></TextField>
                        <div className={classes.buttons}>
                            <input
                                type="file"
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