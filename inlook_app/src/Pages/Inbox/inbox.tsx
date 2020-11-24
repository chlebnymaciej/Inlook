import { IconButton, TextField, Typography } from '@material-ui/core';
import Button from '@material-ui/core/Button';
import List from '@material-ui/core/List';
import DateRangeIcon from '@material-ui/icons/DateRange';
import React, { useEffect, useState } from 'react';
import * as mailApi from '../../Api/mailApi';
import { EmailProps } from '../../Api/mailApi';
import EmailCard from './emailCard';
import EmailContent from './emailContent';


const Inbox = () => {
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
    const emails = getEmails()
    mailApi.getMails().then(r => {
      const emails: EmailProps[] = r.data || [];
      setAllEmails(emails.sort((a, b) => {
        if (a.read === b.read) {
          if (a.sendTime <= b.sendTime) return -1;
          else return 1;
        }
        if (a.read) return 1;
        return -1;
      }));
      setSelectedEmail(r.data ? r.data.length > 0 ? r.data[0] : undefined : undefined);
      setSelectedIndex(r.data ? r.data.length > 0 ? 0 : -1 : -1);
    })
  }, [])

  useEffect(() => {
    setEmails(allEmails);
  }, [allEmails])

  const getEmails = () => {
    return mailApi.getMails(); //TODO: zmiana na API
  }

  const sortDateDescending = (a: EmailProps, b: EmailProps) => {
    if (a.sendTime < b.sendTime) return -1;
    if (a.sendTime > b.sendTime) return 1;
    return 0;
  }

  const sortDateAscending = (a: EmailProps, b: EmailProps) => {
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
        <List style={{ overflowY: 'scroll', height: '94%', width: '100%' }}>
          {emails.map((email, index) =>
            <EmailCard
              email={email}
              handleClick={(email) => handleEmailCardClick(email, index)}
              handleChangeRead={changeRead}
              index={index} />
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