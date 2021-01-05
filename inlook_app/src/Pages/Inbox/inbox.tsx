import { IconButton, TextField } from '@material-ui/core';
import Button from '@material-ui/core/Button';
import List from '@material-ui/core/List';
import DateRangeIcon from '@material-ui/icons/DateRange';
import { useSnackbar } from 'notistack';
import React, { useEffect, useState } from 'react';
import * as mailApi from '../../Api/mailApi';
import { EmailProps } from '../../Api/mailApi';
import EmailCard from './emailCard';
import EmailContent from './emailContent';


const Inbox = () => {
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [counter, SetCounter] = React.useState(0);
  const iCounter = () => SetCounter(counter + 1);
  const [selectedIndex, setSelectedIndex] = React.useState(-1);
  const [selectedEmail, setSelectedEmail] = React.useState<EmailProps>();
  const [allEmails, setAllEmails] = useState<EmailProps[]>([]);
  const [emails, setEmails] = useState<EmailProps[]>([]);
  const handleListItemClick = (
    index: number,
  ) => {
    setSelectedIndex(index);
  };
  
  useEffect(() => {
    mailApi.getMails().then(r => {
      if (r.isError) {
        enqueueSnackbar("Could not retrive emails", { variant: "error" });
        return;
      }
      let emails: EmailProps[] = r.data || [];
      emails = emails.map(e => { return { ...e, sendTime: new Date(e.sendTime) } });
      setAllEmails(emails.sort(sortDateDescending));
      // setAllEmails(emails.sort((a, b) => {
      //   if (a.read === b.read) {
      //     if (a.sendTime <= b.sendTime) return -1;
      //     else return 1;
      //   }
      //   if (a.read) return 1;
      //   return -1;
      // }));
      setSelectedEmail(r.data ? r.data.length > 0 ? r.data[0] : undefined : undefined);
      setSelectedIndex(r.data ? r.data.length > 0 ? 0 : -1 : -1);
      
    })
  }, [])

  useEffect(() => {
    setEmails(allEmails);
  }, [allEmails]);

  const sortDateAscending = (a: EmailProps, b: EmailProps) => {
    if (a.sendTime < b.sendTime) return -1;
    if (a.sendTime > b.sendTime) return 1;
    return 0;
  }

  const sortDateDescending = (a: EmailProps, b: EmailProps) => {
    if (a.sendTime < b.sendTime) return 1;
    if (a.sendTime > b.sendTime) return -1;
    return 0;
  }

  const handleSort = () => {
    let sortedEmails = counter % 2 == 0 ? allEmails.sort(sortDateAscending) : allEmails.sort(sortDateDescending);
    iCounter();
    setAllEmails(sortedEmails);
  }

  const changeRead = (index: number) => {
    const email = allEmails[index];
    setAllEmails(allEmails.map(m => {
      if (m.mailId === email.mailId) {
        return {
          ...m,
          read: !m.read,
        }
      }
      else {
        return m;
      }
    }));

    mailApi.setReadMailStatus(email.mailId, !email.read);

  }

  const handleEmailCardClick = (email: EmailProps, index: number) => {
    setSelectedIndex(index);
    setSelectedEmail(email);
    if (!email.read) {
      changeRead(index);
    }
  }

  const handleFilterChange = (event: any) => {
    const value: string = event.target.value;
    setEmails(allEmails.filter(e => {
      return (e.from.name.toLowerCase().includes(value.toLowerCase()) ||
        e.from.email.toLowerCase().includes(value.toLowerCase()) ||
        e.subject.toLowerCase().includes(value.toLowerCase()))
    }))
  }
const today:Date = new Date();
let todayUsed=false;
let monthUsed=false;
let yearUsed=false;
let evenLater=false;
let weekUsed=false;
  return (
    <div style={{ display: 'flex', height: '87%' }}>
      <div style={{ width: '28%' }} >
        <div className="buttony na sortowanie" style={{ backgroundColor: 'lightgrey' }}>
          Date sorting: 
         <Button onClick={handleSort} style={{ width: '50%' }}>
            {counter % 2 == 0 ? "descending" : "ascending"}
          </Button>
          <IconButton>
            <DateRangeIcon aria-label="choose date" />
          </IconButton>
        </div>
        <TextField onChange={handleFilterChange} label={"Search"} style={{ marginLeft: '0.3em', marginRight: "0.7em", width: "90%" }} />
        <List style={{ overflowY: 'scroll', height: '88%', width: '100%' }}>
          {emails.map((email, index) => {
            if ( todayUsed===false && email.sendTime.getDate()===today.getDate() &&email.sendTime.getMonth()===today.getMonth() &&email.sendTime.getFullYear()===today.getFullYear()) {
              todayUsed = true;
              return (<>
                <div style={{ textAlign: 'center', fontWeight: 'bold' }} >Today: {email.sendTime.getDate() + "." + email.sendTime.getMonth()}</div>
                
                <EmailCard
                  email={email}
                  handleClick={(email) => handleEmailCardClick(email, index)}
                  handleChangeRead={changeRead}
                  index={index} />
              </>)
            }
            else if(email.sendTime.getDate()===today.getDate() &&email.sendTime.getMonth()===today.getMonth() &&email.sendTime.getFullYear()===today.getFullYear())
            {
              return (<>
                <EmailCard
                  email={email}
                  handleClick={(email) => handleEmailCardClick(email, index)}
                  handleChangeRead={changeRead}
                  index={index} />
              </>)
            }
            if ( weekUsed===false && email.sendTime.getDate()+7>=today.getDate() &&email.sendTime.getMonth()===today.getMonth() &&email.sendTime.getFullYear()===today.getFullYear()) {
              weekUsed = true;
              let weekAgo = today.getDate() -7;
              return (<>
                <div style={{ textAlign: 'center', fontWeight: 'bold' }}>This week: {weekAgo + " - "+ today.getDate () + "."+ email.sendTime.getMonth()}</div>

                <EmailCard
                  email={email}
                  handleClick={(email) => handleEmailCardClick(email, index)}
                  handleChangeRead={changeRead}
                  index={index} />
              </>)
            }
            else if(email.sendTime.getDate()+7>=today.getDate() &&email.sendTime.getMonth()===today.getMonth() &&email.sendTime.getFullYear()===today.getFullYear())
            {
              return (<>
                <EmailCard
                  email={email}
                  handleClick={(email) => handleEmailCardClick(email, index)}
                  handleChangeRead={changeRead}
                  index={index} />
              </>)
            }
            else if ( monthUsed===false && email.sendTime.getMonth()===today.getMonth() &&email.sendTime.getFullYear()===today.getFullYear()) {
              monthUsed = true;
              return (<>
                <div style={{ textAlign: 'center', fontWeight: 'bold' }}>This month: {email.sendTime.getMonth() + "." + email.sendTime.getFullYear()}</div>
                <EmailCard
                  email={email}
                  handleClick={(email) => handleEmailCardClick(email, index)}
                  handleChangeRead={changeRead}
                  index={index} />
              </>)
            }
            else if(email.sendTime.getMonth()===today.getMonth() &&email.sendTime.getFullYear()===today.getFullYear())
            {
              return (<>
                <EmailCard
                  email={email}
                  handleClick={(email) => handleEmailCardClick(email, index)}
                  handleChangeRead={changeRead}
                  index={index} />
              </>)
            }
            else if ( yearUsed===false && email.sendTime.getFullYear()===today.getFullYear()) {
              yearUsed = true;
              return (<>
                <div style={{ textAlign: 'center', fontWeight: 'bold' }}  >This year: {email.sendTime.getMonth()}</div>

                <EmailCard
                  email={email}
                  handleClick={(email) => handleEmailCardClick(email, index)}
                  handleChangeRead={changeRead}
                  index={index} />
              </>)
            }
            else if(email.sendTime.getFullYear()===today.getFullYear())
            {
              return (<>
                <EmailCard
                  email={email}
                  handleClick={(email) => handleEmailCardClick(email, index)}
                  handleChangeRead={changeRead}
                  index={index} />
              </>)
            }
            else if ( evenLater===false) {
              evenLater = true;
              return (<>
                <div>Later: </div>

                <EmailCard
                  email={email}
                  handleClick={(email) => handleEmailCardClick(email, index)}
                  handleChangeRead={changeRead}
                  index={index} />
              </>)
            }
            else{
              return <EmailCard
              email={email}
              handleClick={(email) => handleEmailCardClick(email, index)}
              handleChangeRead={changeRead}
              index={index} />
            }           
          }
          )}

        </List>
      </div>
      <div style={{ width: '2%', backgroundColor: 'white' }}>
      </div>
      {selectedEmail &&
        <div style={{ width: '70%', marginTop: '2%' }}>
          <EmailContent email={selectedEmail} />
        </div>
      }
    </div>
  )
}

export default Inbox;