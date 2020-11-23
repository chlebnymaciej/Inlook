import { IconButton } from '@material-ui/core';
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
  const [emails, setEmails] = useState<EmailProps[]>([]);
  const handleListItemClick = (
    index: number,
  ) => {
    setSelectedIndex(index);
  };

  useEffect(() => {
    const emails = getEmails()
    mailApi.getMails().then(r => {
      setEmails(r.data || []);
      setSelectedEmail(r.data ? r.data.length > 0 ? r.data[0] : undefined : undefined);
      setSelectedIndex(r.data ? r.data.length > 0 ? 0 : -1 : -1);
    })
  }, [])

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
    let sortedEmails = counter % 2 == 0 ? emails.sort(sortDateAscending) : emails.sort(sortDateDescending);
    iCounter();
    setEmails(sortedEmails);
  }

  const changeRead = (index: number) => {
    const email = emails[index];
    setEmails(emails.map(m => {
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