import React, { useState } from 'react';
import Avatar from '@material-ui/core/Avatar';
import { Divider, IconButton, ListItem } from '@material-ui/core';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import ListItemText from '@material-ui/core/ListItemText';
import Typography from '@material-ui/core/Typography';
import ReplyIcon from '@material-ui/icons/Reply';
import Button from '@material-ui/core/Button';
import ArrowForwardIcon from '@material-ui/icons/ArrowForward';
import List from '@material-ui/core/List';
import DateRangeIcon from '@material-ui/icons/DateRange';
import { EmailProps } from './inbox';

export interface EmailContentProps {
    email: EmailProps;
}

const EmailContent = (props: EmailContentProps) => {
    return (
        <div style={{ height: '100%', width: '100%' }}>
            <header style={{ alignSelf: 'center', fontWeight: 'bold', fontSize: '1.5rem', marginLeft: '5%', marginBottom: '2%' }}>
                {props.email.subject}
            </header>
            <div style={{ display: 'flex', height: '90%', justifyContent: 'felxstart', }}>
                <ListItemAvatar >
                    <Avatar alt={props.email.from.name} src="/static/images/avatar/1.jpg" />
                </ListItemAvatar>
                <div className="TextSections" style={{ display: 'flex', flexDirection: 'column', width: '100%' }}>
                    <div style={{ fontWeight: 'bold', fontSize: '1 rem', height: '10%' }}>
                        {props.email.from.name}
                    </div>
                    <div style={{ height: '85%', width: '100%', overflowY: 'scroll' }}>
                        {props.email.text}
                    </div>
                    <div style={{ height: '5%' }}>
                        <Button startIcon={<ReplyIcon />} style={{ border: 'solid', marginRight: '3%', borderWidth: '2px', borderColor: 'lightgrey' }}>
                            Odpowiedz
              </Button>
                        <Button startIcon={<ArrowForwardIcon />} style={{ border: 'solid', borderWidth: '2px', borderColor: 'lightgrey' }}>
                            Przekaż dalej
              </Button>
                    </div>
                </div>
            </div>

        </div>
    )

}

export default EmailContent;