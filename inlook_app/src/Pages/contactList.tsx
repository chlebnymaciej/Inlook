import { Checkbox, createStyles, IconButton, List, ListItem, ListItemIcon, ListItemSecondaryAction, makeStyles, TextField, Theme } from '@material-ui/core';
import ListItemText from '@material-ui/core/ListItemText/ListItemText';
import CommentIcon from '@material-ui/icons/Comment';
import Pagination from '@material-ui/lab/Pagination';
import React, { useEffect, useState } from 'react';
import * as userApi from '../Api/userApi';
import { Contact } from '../Api/userApi';

export interface ContactListProps {

}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      width: '100%',
      backgroundColor: theme.palette.background.paper,
    },
    searchField: {
        width: "30%",
        margin: theme.spacing(2),
    }
  }),
);

const ContactList = (props :ContactListProps ) => {
    const classes = useStyles();

    const [contacts, setContacts] = useState<Contact[]>();
    const [error, setError] = useState<string>();
    const [checked, setChecked] = React.useState<string[]>([]);
    const [page, setPage] = React.useState<number>(1);
    const [totalPages, setTotalPages] = React.useState<number>(0);
    const [searchText, setSearchText] = React.useState<string>("");
    const [searchTextTimeout, setSearchTextTimeout] = React.useState<NodeJS.Timeout>();

    useEffect(() => {
        getPage(1,searchText);
    },[]);

    const handlePageChange = (event: React.ChangeEvent<unknown>, pageNumber: number) => {
      setPage(pageNumber);
      getPage(pageNumber,searchText);
    };

    const getPage = (pageNumber:number, searchText:string) => {
        userApi.getContactList(pageNumber,2,searchText)
        .then(r => {
            if(r.isError){
                setError(r.errorMessage);
                return;
            }
            setContacts(r.data?.contacts);
            setTotalPages(r.data?.totalPages || 0)
        })
    }

    const handleToggle = (mail: string) => () => {
        const currentIndex = checked.indexOf(mail);
        const newChecked = [...checked];
    
        if (currentIndex === -1) {
          newChecked.push(mail);
        } else {
          newChecked.splice(currentIndex, 1);
        }
    
        setChecked(newChecked);
      };

    const handleSearchTextChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value;
        setSearchText(value);
        if(searchTextTimeout) clearTimeout(searchTextTimeout);
        let newTimeout = setTimeout(() => {
            getPage(page, value);
        }, 300);
        setSearchTextTimeout(newTimeout);

    }

    return (
        <>
        <TextField onChange={handleSearchTextChange} className={classes.searchField} label={"Wyszukaj"}/>
        <List className={classes.root}>
          {contacts?.map((contact, index) => {
            const labelId = `checkbox-list-label-${contact.email}`;
    
            return (
              <ListItem key={contact.email} role={undefined} dense button onClick={handleToggle(contact.email)}>
                <ListItemIcon>
                  <Checkbox
                    edge="start"
                    checked={checked.indexOf(contact.email) !== -1}
                    tabIndex={-1}
                    disableRipple
                    inputProps={{ 'aria-labelledby': labelId }}
                  />
                </ListItemIcon>
                <ListItemText id={labelId} primary={`${contact.name} ${contact.email}`} />
                <ListItemSecondaryAction>
                  <IconButton edge="end" aria-label="comments">
                    <CommentIcon />
                  </IconButton>
                </ListItemSecondaryAction>
              </ListItem>
            );
          })}
        </List>
         <Pagination count={totalPages} page={page} onChange={handlePageChange} />
         </>
      );

}
export default ContactList;