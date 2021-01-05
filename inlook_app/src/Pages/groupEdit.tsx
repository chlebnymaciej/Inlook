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
import { GroupModel, updateGroup } from "../Api/groupsApi";
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
  error: {
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

interface GroupEditProps {
  user: User;
  group: GroupModel;
}

const EditGroup = (props: any) => {
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const group: GroupEditProps =
    (props.location && props.location.state) || {};
  const classes = useStyles();
  const history = useHistory();
  const [checked, setChecked] = useState<UserModel[]>([]);
  const [leftSideUsers, setLeftUsers] = useState<UserModel[]>([]);
  const [rightSideUsers, setRightUsers] = useState<UserModel[]>(group.group.users || []);
  const [errorText, setErrorText] = useState<string>('');
  const [groupName, setGroupName] = useState<string>(group.group.name);

  const leftSideCheckedUsers: UserModel[] = checked.filter((x: UserModel) => leftSideUsers.includes(x));
  const rightSideCheckedUsers: UserModel[] = checked.filter((x: UserModel) => rightSideUsers.includes(x));

  useEffect(() => {
    getUsers().then(result => {
      if (result.isError) {
        enqueueSnackbar("Could not load contacts", { variant: "error" });
      }
      else {
        let users = group.group.users.map(z => z.id);
        let leftTmp = result.data?.filter(x => {
          return users.includes(x.id) === false
        });

        setLeftUsers(leftTmp || []);
      }
    })
  }, [props.user]);

  const handleToggle = (value: UserModel) => () => {
    const currentIndex = checked.indexOf(value);
    const newlyChecked = [...checked];

    if (currentIndex === -1) {
      newlyChecked.push(value);
    } else {
      newlyChecked.splice(currentIndex, 1);
    }

    setChecked(newlyChecked);
  };

  const SideList = (users: UserModel[]) => (
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
    setRightUsers(rightSideUsers.concat(leftSideUsers));
    setLeftUsers([]);
  };
  const handleAllLeftClick = () => {
    setLeftUsers(leftSideUsers.concat(rightSideUsers));
    setRightUsers([]);
  };

  const handleCheckedUsersRightClick = () => {
    setRightUsers(rightSideUsers.concat(leftSideCheckedUsers));
    setLeftUsers(leftSideUsers.filter((x: UserModel) => !leftSideCheckedUsers.includes(x)));
    setChecked([]);
  };

  const handleCheckedUsersLeftClick = () => {
    setLeftUsers(leftSideUsers.concat(rightSideCheckedUsers));
    setRightUsers(rightSideUsers.filter((x: UserModel) => !rightSideCheckedUsers.includes(x)));
    setChecked([]);
  };
  const handleSubmit = (e: any) => {
    e.preventDefault();
    if (rightSideUsers.length === 0) {
      setErrorText('List of user in group cannot be empty.');
      return;
    }
    if (!groupName) {
      setErrorText('Group name cannot be empty.');
      return;
    }

    updateGroup(
      {
        id: group.group.id,
        name: groupName,
        users: rightSideUsers.map(x => x.id),
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
          defaultValue={group.group.name}
          className={classes.nameField}
          onChange={(e) => setGroupName(e.target.value)}
        ></TextField>
        {errorText ? <FormHelperText className={classes.error}>{errorText}</FormHelperText> : <></>}

        <Grid container spacing={2} justify="center" alignItems="center" className={classes.grid}>
          <Grid item>{SideList(leftSideUsers)}</Grid>
          <Grid item>
            <Grid container direction="column" alignItems="center">
              <Button
                variant="outlined"
                size="small"
                disabled={leftSideUsers.length === 0}
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
                disabled={rightSideUsers.length === 0}
                aria-label="move all left"
                onClick={handleAllLeftClick}
              >
                ≪
          </Button>
            </Grid>
          </Grid>
          <Grid item>{SideList(rightSideUsers)}</Grid>
        </Grid>
        <Button className={classes.button} type="submit">Update group</Button>
      </form>
    </div>
  );
};

export default EditGroup;