import { FormHelperText, List, ListItem, Paper, TextField } from "@material-ui/core";
import Button from "@material-ui/core/Button/Button";
import Checkbox from "@material-ui/core/Checkbox/Checkbox";
import Grid from "@material-ui/core/Grid/Grid";
import ListItemIcon from "@material-ui/core/ListItemIcon/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText/ListItemText";
import { makeStyles } from '@material-ui/core/styles';
import { useSnackbar } from "notistack";
import { User } from "oidc-client";
import React, { useEffect, useState } from "react";
import { useHistory } from "react-router";
import { postGroup } from "../Api/groupsApi";
import { getUsers, UserModel } from "../Api/userApi";

const useStyles = makeStyles({
  root: {
    display: "flex",
    flexDirection: "column",
    width: "80%",
    margin: "1em auto auto auto",
    color: "black"
  },
  grid: {
    width: "80%",
    margin: "1em auto auto auto",
    color: "black"
  },
  paper: {
    width: "25em",
    overflow: 'auto',
  },
  nameField: {
    width: "50em",
    margin: "auto"
  },
  errorClass: {
    margin: "auto",
    color: "red"
  },
  button: {
    background: "#487EB0",
    color: "white",
    width: "15em",
    margin: "auto",
    '&:hover': {
      backgroundColor: "#DAE0E2",
      color: "black"
    }
  }
});

interface CreateGroupsProps {
  user: User | null;
}

const CreateGroups = (props: CreateGroupsProps) => {
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [checked, setChecked] = useState<UserModel[]>([]);
  const [leftUsers, setLeftUsers] = useState<UserModel[]>([]);
  const [rightUsers, setRightUsers] = useState<UserModel[]>([]);
  const [errorText, setErrorText] = useState<string>('');
  const [groupName, setGroupName] = useState<string>('');

  const history = useHistory();
  const classes = useStyles();

  const leftSideCheckedUsers: UserModel[] = checked.filter((x: UserModel) => leftUsers.includes(x));
  const rightSideCheckedUsers: UserModel[] = checked.filter((x: UserModel) => rightUsers.includes(x));

  useEffect(() => {
    getUsers().then(result => {
      if (result.isError) {
        enqueueSnackbar("Could not load contacts", { variant: "error" });
      }
      else {
        setLeftUsers(result.data || []);
      }
    })
  }, [props.user]);

  const handleToggle = (value: UserModel) => () => {
    const currentIndex = checked.indexOf(value);
    const newChecked = [...checked];

    if (currentIndex === -1) {
      newChecked.push(value);
    } else {
      newChecked.splice(currentIndex, 1);
    }

    setChecked(newChecked);
  };

  const customList = (users: UserModel[]) => (
    <Paper className={classes.paper}>
      <List dense component="div" role="list">
        {users.map((x: UserModel) => {
          const labelId = `transfer-list-item-${x.email}-label`;
          return (
            <ListItem key={users.indexOf(x)} role="listitem" button onClick={handleToggle(x)}>
              <ListItemIcon>
                <Checkbox
                  checked={checked.indexOf(x) !== -1}
                  tabIndex={-1}
                  disableRipple
                  inputProps={{ 'aria-labelledby': labelId }}
                />
              </ListItemIcon>
              <ListItemText id={labelId} primary={x.name.concat(" ").concat(x.email)} />
            </ListItem>
          );
        })}
        <ListItem />
      </List>
    </Paper>
  );

  const handleAllRightClick = () => {
    setRightUsers(rightUsers.concat(leftUsers));
    setLeftUsers([]);
  };
  const handleAllLeftClick = () => {
    setLeftUsers(leftUsers.concat(rightUsers));
    setRightUsers([]);
  };

  const handleCheckedUsersRightClick = () => {
    setRightUsers(rightUsers.concat(leftSideCheckedUsers));
    setLeftUsers(leftUsers.filter((x: UserModel) => !leftSideCheckedUsers.includes(x)));
    setChecked([]);
  };

  const handleCheckedUsersLeftClick = () => {
    setLeftUsers(leftUsers.concat(rightSideCheckedUsers));
    setRightUsers(rightUsers.filter((x: UserModel) => !rightSideCheckedUsers.includes(x)));
    setChecked([]);
  };
  const handleSubmit = async (e: any) => {
    e.preventDefault();
    if (rightUsers.length === 0) {
      setErrorText('List of user in group cannot be empty.');
      return;
    }
    if (!groupName) {
      setErrorText('Group name cannot be empty.');
      return;
    }

    postGroup({
      name: groupName,
      users: rightUsers.map(x => x.id)
    }).then(r => {
      if (r.isError) {
        enqueueSnackbar("Something went wrong", { variant: "error" });
      }
      else {
        history.push('/groups');
      }
    });

  };
  return (
    <div>
      <form className={classes.root} onSubmit={handleSubmit}>
        <TextField
          label="Group Name"
          required
          className={classes.nameField}
          onChange={(e) => setGroupName(e.target.value)}
        ></TextField>
        {errorText ? <FormHelperText className={classes.errorClass}>{errorText}</FormHelperText> : <></>}

        <Grid container spacing={2} justify="center" alignItems="center" className={classes.grid}>
          <Grid item>{customList(leftUsers)}</Grid>
          <Grid item>
            <Grid container direction="column" alignItems="center">
              <Button
                variant="outlined"
                size="small"
                disabled={leftUsers.length === 0}
                onClick={handleAllRightClick}
                aria-label="move all right"
              >
                ≫
          </Button>
              <Button
                variant="outlined"
                size="small"
                aria-label="move selected right"
                onClick={handleCheckedUsersRightClick}
                disabled={leftSideCheckedUsers.length === 0}
              >
                &gt;
          </Button>
              <Button
                variant="outlined"
                size="small"
                aria-label="move selected left"
                onClick={handleCheckedUsersLeftClick}
                disabled={rightSideCheckedUsers.length === 0}

              >
                &lt;
          </Button>
              <Button
                variant="outlined"
                size="small"
                disabled={rightUsers.length === 0}
                aria-label="move all left"
                onClick={handleAllLeftClick}
              >
                ≪
          </Button>
            </Grid>
          </Grid>
          <Grid item>{customList(rightUsers)}</Grid>
        </Grid>
        <Button className={classes.button} type="submit">Create group</Button>
      </form>
    </div>
  );
};

export default CreateGroups;