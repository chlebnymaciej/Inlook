import { Button } from "@material-ui/core";
import { User } from "oidc-client";
import React from "react";
import { useHistory } from "react-router";

import ClickAwayListener from '@material-ui/core/ClickAwayListener';
import Grow from '@material-ui/core/Grow';
import Paper from '@material-ui/core/Paper';
import Popper from '@material-ui/core/Popper';
import MenuItem from '@material-ui/core/MenuItem';
import MenuList from '@material-ui/core/MenuList';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import MenuIcon from '@material-ui/icons/Menu';
import SendIcon from '@material-ui/icons/Send';
import MailOutlineIcon from '@material-ui/icons/MailOutline';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      display: 'flex',
      margin:"1em",
    },
    paper: {
      marginRight: theme.spacing(2),
    },
    button:{
        backgroundColor: "white",
        '&:hover':{
            backgroundColor:"#DAE0E2"
        }
    },
    icon:{
        marginLeft:"1em"
    },
    mitem:{
        display:"flex",
        justifyContent:"space-between"
    }
  }),
);

  interface MenuButtonProps {
      user: User | null;
  }
    
const MenuButton = (props: MenuButtonProps) => {
    const classes = useStyles();
    const [open, setOpen] = React.useState(false);
    const anchorRef = React.useRef<HTMLButtonElement>(null);
    const useHist = useHistory();
    const handleToggle = () => {
      setOpen((prevOpen) => !prevOpen);
    };
  
    const handleClose = (event: React.MouseEvent<EventTarget>) => {
      if (anchorRef.current && anchorRef.current.contains(event.target as HTMLElement)) {
        return;
      }
  
      setOpen(false);
    };
  
    function handleListKeyDown(event: React.KeyboardEvent) {
      if (event.key === 'Tab') {
        event.preventDefault();
        setOpen(false);
      }
    }

    const prevOpen = React.useRef(open);
    React.useEffect(() => {
      if (prevOpen.current === true && open === false) {
        anchorRef.current!.focus();
      }
  
      prevOpen.current = open;
    }, [open]);

    return (
        <>
        {
            props.user ? 
            <div className={classes.root}>
            <div>
              <Button
                className={classes.button}
                ref={anchorRef}
                aria-controls={open ? 'menu-list-grow' : undefined}
                aria-haspopup="true"
                onClick={handleToggle}
              >
                  <MenuIcon
                  ></MenuIcon>
              </Button>
              <Popper open={open} anchorEl={anchorRef.current} role={undefined} transition disablePortal>
                {({ TransitionProps, placement }) => (
                  <Grow
                    {...TransitionProps}
                    style={{ transformOrigin: placement === 'bottom' ? 'center top' : 'center bottom' }}
                  >
                    <Paper>
                      <ClickAwayListener onClickAway={handleClose}>
                        <MenuList autoFocusItem={open} id="menu-list-grow" onKeyDown={handleListKeyDown}>
                          <MenuItem className={classes.mitem} onClick={(e)=>{handleClose(e); useHist.push('/home');}}>

                            <div>Inbox</div>
                          <MailOutlineIcon className={classes.icon}></MailOutlineIcon>
                          </MenuItem>
                          <MenuItem className={classes.mitem} onClick={(e)=>{handleClose(e); useHist.push('/newmessage');}}>
                          <div>New Mail</div>
                          <SendIcon className={classes.icon}></SendIcon>
                          </MenuItem>
                        </MenuList>
                      </ClickAwayListener>
                    </Paper>
                  </Grow>
                )}
              </Popper>
            </div>
          </div>


            :<></>
        }
        </>
            
    )
}

export default MenuButton;