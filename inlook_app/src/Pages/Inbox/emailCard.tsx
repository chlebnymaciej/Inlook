import { Divider, ListItem } from '@material-ui/core';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import ListItemText from '@material-ui/core/ListItemText';
import Typography from '@material-ui/core/Typography';
import React from 'react';
import { EmailProps } from '../../Api/mailApi';

export interface EmailCardProps {
    email: EmailProps;
    handleClick: (email: EmailProps) => void;
    handleChangeRead: (index: number) => void;
    index: number;
}

const EmailCard = (props: EmailCardProps) => {
    return (
        <div >
            <ListItem button onClick={() => props.handleClick(props.email)}
                style={{ display: 'flex ', flexWrap: 'wrap', backgroundColor: props.email.read ? 'lightgrey' : 'white', }}  >
                <div style={{ display: 'flex ' }}>
                    <ListItemAvatar>
                        <Avatar alt={props.email.from.name} src="/static/images/avatar/1.jpg" />
                    </ListItemAvatar>
                    <Button onClick={() => props.handleChangeRead(props.index)} style={{ backgroundColor: props.email.read ? 'white' : 'lightgrey' }}>
                        {props.email.read ? "seen" : "not seen"}
                    </Button>
                </div>

                <ListItemText
                    primary={props.email.subject || ""}
                    secondary={
                        <React.Fragment>
                            <Typography
                                component="span"
                                variant="body2"
                                style={{ display: 'inline', }}
                                color="textPrimary"
                            >
                                {props.email.from.name || ""}
                            </Typography>
                :  {(props.email.text || "").substring(0, 40) + "..."}
                        </React.Fragment>
                    }
                />
                <div style={{ alignSelf: 'center', fontSize: '12px' }}>
                    {props.email.sendTime?.toLocaleString() || ""}
                </div>
            </ListItem>
            <Divider />
        </div>

    )
}

export default EmailCard;