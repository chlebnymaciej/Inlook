import React, { useEffect, useState } from 'react';
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
import EmailCard from './emailCard';
import EmailContent from './emailContent';
import { UserModel } from '../../Api/userApi';

export interface EmailProps {
  subject: string;
  from: UserModel;
  to: UserModel[];
  cc: UserModel[];
  sendTime: Date;
  text: string;
  read: boolean;
  mailId: string;
}

const TestData: EmailProps[] = [
  { subject: "Brunch this weekend?", from: { name: "Ali Connors", email: "eee@EE.e", id: "1234-1234-1234-1234" }, to: [], cc: [], sendTime: new Date(1999, 10, 17, 15, 15, 30), text: "naszym celem jest opanowanie swiataaaaaaaaaaaaaaaaaaaaaaaaaa", read: true, mailId: "123-123-123-123" },
  { subject: "Halo Halo budowa wjezdza", from: { name: "Peja Rychu", email: "eee@EE.e", id: "1234-1234-1234-1234" }, to: [], cc: [], sendTime: new Date(2000, 10, 17, 15, 15, 30), text: "997 wiadomo", read: true, mailId: "124-123-123-123" },
  { subject: "Piekny kodzik", from: { name: "Ali Connors", email: "eee@EE.e", id: "1234-1234-1234-1234" }, to: [], cc: [], sendTime: new Date(20015, 10, 17, 15, 15, 30), text: "naszym celem jest opanowanie swiata", read: false, mailId: "125-123-123-123" },
  { subject: "Flanki gramy dzisiaj", from: { name: "Ali Connors", email: "eee@EE.e", id: "1234-1234-1234-1234" }, to: [], cc: [], sendTime: new Date(2001, 10, 17, 15, 15, 30), text: "naszym celem jest opanowanie swiata", read: true, mailId: "126-123-123-123" },
  { subject: "Kurczaki", from: { name: "Ali Connors", email: "eee@EE.e", id: "1234-1234-1234-1234" }, to: [], cc: [], sendTime: new Date(2005, 10, 17, 15, 15, 30), text: "naszym celem jest opanowanie swiata", read: false, mailId: "127-123-123-123" },
  { subject: "Ziemniaki", from: { name: "Ali Connors", email: "eee@EE.e", id: "1234-1234-1234-1234" }, to: [], cc: [], sendTime: new Date(2002, 10, 17, 15, 15, 30), text: "naszym celem jest opanowanie swiata", read: true, mailId: "128-123-123-123" },
  { subject: "Klaun fiesta", from: { name: "Ali Connors", email: "eee@EE.e", id: "1234-1234-1234-1234" }, to: [], cc: [], sendTime: new Date(2008, 10, 17, 15, 15, 30), text: "naszym celem jest opanowanie swiata", read: true, mailId: "129-123-123-123" },
  { subject: "Stoooo", from: { name: "Ali Connors", email: "eee@EE.e", id: "1234-1234-1234-1234" }, to: [], cc: [], sendTime: new Date(2007, 10, 17, 15, 15, 30), text: "naszym celem jest opanowanie swiata", read: true, mailId: "130-123-123-123" },
  { subject: "JEdziemy", from: { name: "Ali Connors", email: "eee@EE.e", id: "1234-1234-1234-1234" }, to: [], cc: [], sendTime: new Date(2003, 10, 17, 15, 15, 30), text: "naszym celem jest opanowanie swiata", read: true, mailId: "131-123-123-123" }
]






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
    setEmails(emails);
    setSelectedEmail(emails.length > 0 ? emails[0] : undefined);
    setSelectedIndex(emails.length > 0 ? 0 : -1);
  }, [])

  const getEmails = () => {
    return TestData; //TODO: zmiana na API
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
    //TODO: api change read
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