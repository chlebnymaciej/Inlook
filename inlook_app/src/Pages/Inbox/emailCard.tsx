import { Divider, ListItem, makeStyles } from '@material-ui/core';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import ListItemText from '@material-ui/core/ListItemText';
import Typography from '@material-ui/core/Typography';
import React from 'react';
import { EmailProps } from '../../Api/mailApi';



const useStyles = makeStyles({
    avatar:{
        display:"flex"
    },
    not_seen:{
        display: 'flex ',
        flexWrap: 'wrap',
        fontWeight:"bold",
        background: "lightgrey"
    },
    seen:{
        display: 'flex ',
        flexWrap: 'wrap',
        background: "white"
    },
    date:{ 
        alignSelf: 'center',
        fontSize: '12px'
    }
});
export interface EmailCardProps {
    email: EmailProps;
    handleClick: (email: EmailProps) => void;
    handleChangeRead: (index: number) => void;
    index: number;
}

const EmailCard = (props: EmailCardProps) => {
    const classes = useStyles();
    let classSeenNotSeen  = classes.not_seen;
    if(props.email.read){
        classSeenNotSeen = classes.seen;
    }
    return (
        <div>
            <ListItem button onClick={() => props.handleClick(props.email)}
                className={ classSeenNotSeen}>
                <div className={classes.avatar}>
                    <ListItemAvatar>
                        <Avatar alt={props.email.from.name} src="/static/images/avatar/1.jpg" />
                    </ListItemAvatar>
                    <Button onClick={() => props.handleChangeRead(props.index)}>
                        {props.email.read ? "seen" : "not seen"}
                    </Button>
                </div>

                <ListItemText
                    style={{fontWeight:"bold"}}
                    primary={props.email.subject || ""}
                    secondary={
                    (<>  
                    {props.email.from.name || ""}:{(props.email.text || "").substring(0, 40) + "..."}
                    </>)
                    }>
                </ListItemText>
                <div className={classes.date}>
                    {props.email.sendTime?.toLocaleString() || ""}
                </div>
            </ListItem>
            <Divider />
        </div>

    )
}

export default EmailCard;