import { FormHelperText, List, ListItem, Paper, TextField } from "@material-ui/core";
import Button from "@material-ui/core/Button/Button";
import Checkbox from "@material-ui/core/Checkbox/Checkbox";
import Grid from "@material-ui/core/Grid/Grid";
import ListItemIcon from "@material-ui/core/ListItemIcon/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText/ListItemText";
import { makeStyles } from '@material-ui/core/styles';
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
  const [error, setError] = useState<string>();
  const [checked, setChecked] = useState<UserModel[]>([]);
  const [left, setLeft] = useState<UserModel[]>([]);
  const [right, setRight] = useState<UserModel[]>([]);
  const [errorText, setErrorText] = useState<string>('');
  const [groupName, setGroupName] = useState<string>('');

  const history = useHistory();
  const classes = useStyles();

  const leftChecked: UserModel[] = checked.filter((x: UserModel) => left.includes(x));
  const rightChecked: UserModel[] = checked.filter((x: UserModel) => right.includes(x));

  useEffect(() => {
    getUsers().then(result => {
      if (result.isError) {
        setError(result.errorMessage);
      }
      else {
        setLeft(result.data || []);
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

  const handleAllRight = () => {
    setRight(right.concat(left));
    setLeft([]);
  };
  const handleAllLeft = () => {
    setLeft(left.concat(right));
    setRight([]);
  };

  const handleCheckedRight = () => {
    setRight(right.concat(leftChecked));
    setLeft(left.filter((x: UserModel) => !leftChecked.includes(x)));
    setChecked([]);
  };

  const handleCheckedLeft = () => {
    setLeft(left.concat(rightChecked));
    setRight(right.filter((x: UserModel) => !rightChecked.includes(x)));
    setChecked([]);
  };
  const handleSubmit = async (e: any) => {
    e.preventDefault();
    if (right.length === 0) {
      setErrorText('List of user in group cannot be empty.');
      return;
    }
    if (!groupName) {
      setErrorText('Group name cannot be empty.');
      return;
    }

    postGroup({
      name: groupName,
      users: right.map(x => x.id)
    }).then(() => {
      history.push('/groups');
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
          <Grid item>{customList(left)}</Grid>
          <Grid item>
            <Grid container direction="column" alignItems="center">
              <Button
                variant="outlined"
                size="small"
                disabled={left.length === 0}
                onClick={handleAllRight}
                aria-label="move all right"
              >
                ≫
          </Button>
              <Button
                variant="outlined"
                size="small"
                aria-label="move selected right"
                onClick={handleCheckedRight}
                disabled={leftChecked.length === 0}
              >
                &gt;
          </Button>
              <Button
                variant="outlined"
                size="small"
                aria-label="move selected left"
                onClick={handleCheckedLeft}
                disabled={rightChecked.length === 0}

              >
                &lt;
          </Button>
              <Button
                variant="outlined"
                size="small"
                disabled={right.length === 0}
                aria-label="move all left"
                onClick={handleAllLeft}
              >
                ≪
          </Button>
            </Grid>
          </Grid>
          <Grid item>{customList(right)}</Grid>
        </Grid>
        <Button className={classes.button} type="submit">Create group</Button>
      </form>
    </div>
  );
};

export default CreateGroups;