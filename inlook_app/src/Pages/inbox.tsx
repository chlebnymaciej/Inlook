import React ,{useState } from 'react';
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
type EmailProps ={
  title?: string,
  author?: string,
  date?: string,
  text?: string,
  alreadyRead?: boolean;
}
const TestData: EmailProps[] = [
  {title: "Brunch this weekend?", author: "Ali Connors", date: "19.11.2020 18:26:46", text: "naszym celem jest opanowanie swiataaaaaaaaaaaaaaaaaaaaaaaaaa",alreadyRead: true},
  {title: "Halo Halo budowa wjezdza", author: "Shmittke", date: "22.11.2020 18:26:46", text: "naszym celem jest opanowanie swiata" ,alreadyRead: true},
  {title: "Piekny kodzik", author: "Piekny koder", date: "15.11.2020 18:26:46", text: "naszym celem jest opanowanie swiata" ,alreadyRead: false},
  {title: "Flanki gramy dzisiaj", author: "Akademik", date: "10.10.2020 19:26:46", text: "naszym celem jest opanowanie swiata" ,alreadyRead: true},
  {title: "Kurczaki", author: "Lasy", date: "19.11.2019 18:26:46", text: "naszym celem jest opanowanie swiata",alreadyRead: false },
  {title: "Ziemniaki", author: "Lasy", date: "19.11.2020 19:26:46", text: "naszym celem jest opanowanie swiata" ,alreadyRead: true},
  {title: "Klaun fiesta", author: "Lasy", date: "19.12.2020 19:26:46", text: "naszym celem jest opanowanie swiata",alreadyRead: true },
  {title: "Stoooo", author: "Lasy", date: "01.11.2020 19:26:46", text: "naszym celem jest opanowanie swiata",alreadyRead: true },
  {title: "JEdziemy", author: "Lasy", date: "19.11.2020 21:37:00", text: "naszym celem jest opanowanie swiata" ,alreadyRead: true}
]
let message: EmailProps = {title: "Brunch this weekend?", author: "Ali Connors", date: "19.11.2020 18:26:46", text: "naszym celem jest opanowanie swiataaaaaaaaaaaaaaaaaaaaaaaaaa",alreadyRead: true};
const getText = (text?: string  ) =>{
  if(text==undefined || text ==null) return "0";
  else return text.toString();
}
const sortAscendingDate = (obj1: EmailProps, obj2:EmailProps)=>{
  if(getText(obj1.date)=="0") return -1;
  if(getText(obj2.date)=="0") return 1;
  //years
  if(parseInt(getText(obj2.date).substring(6,10))>parseInt(getText(obj1.date).substring(6,10))) return 1;
  else if(parseInt(getText(obj2.date).substring(6,10))<parseInt(getText(obj1.date).substring(6,10))) return -1;
  //months
  if(parseInt(getText(obj2.date).substring(3,5))>parseInt(getText(obj1.date).substring(3,5))) return 1;
  else if(parseInt(getText(obj2.date).substring(3,5))<parseInt(getText(obj1.date).substring(3,5))) return -1;
  //days
  if(parseInt(getText(obj2.date).substring(0,2))>parseInt(getText(obj1.date).substring(0,2))) return 1;
  else if(parseInt(getText(obj2.date).substring(0,2))<parseInt(getText(obj1.date).substring(0,2))) return -1;
  //hours
  if(parseInt(getText(obj2.date).substring(11,13))>parseInt(getText(obj1.date).substring(11,13))) return 1;
  else if(parseInt(getText(obj2.date).substring(11,13))<parseInt(getText(obj1.date).substring(11,13))) return -1;
  //minutes
  if(parseInt(getText(obj2.date).substring(14,16))>parseInt(getText(obj1.date).substring(14,16))) return 1;
  else if(parseInt(getText(obj2.date).substring(14,16))<parseInt(getText(obj1.date).substring(14,16))) return -1;
  //seconds
  if(parseInt(getText(obj2.date).substring(17,19))>parseInt(getText(obj1.date).substring(17,19))) return 1;
  else if(parseInt(getText(obj2.date).substring(17,19))<parseInt(getText(obj1.date).substring(17,19))) return -1;
  return 0;
}
const sortDescendingDate = (obj1: EmailProps, obj2:EmailProps)=>{
  if(getText(obj1.date)=="0") return -1;
  if(getText(obj2.date)=="0") return 1;
  //years
  if(parseInt(getText(obj2.date).substring(6,10))>parseInt(getText(obj1.date).substring(6,10))) return -1;
  else if(parseInt(getText(obj2.date).substring(6,10))<parseInt(getText(obj1.date).substring(6,10))) return 1;
  //months
  if(parseInt(getText(obj2.date).substring(3,5))>parseInt(getText(obj1.date).substring(3,5))) return -1;
  else if(parseInt(getText(obj2.date).substring(3,5))<parseInt(getText(obj1.date).substring(3,5))) return 1;
  //days
  if(parseInt(getText(obj2.date).substring(0,2))>parseInt(getText(obj1.date).substring(0,2))) return -1;
  else if(parseInt(getText(obj2.date).substring(0,2))<parseInt(getText(obj1.date).substring(0,2))) return 1;
  //hours
  if(parseInt(getText(obj2.date).substring(11,13))>parseInt(getText(obj1.date).substring(11,13))) return -1;
  else if(parseInt(getText(obj2.date).substring(11,13))<parseInt(getText(obj1.date).substring(11,13))) return 1;
  //minutes
  if(parseInt(getText(obj2.date).substring(14,16))>parseInt(getText(obj1.date).substring(14,16))) return -1;
  else if(parseInt(getText(obj2.date).substring(14,16))<parseInt(getText(obj1.date).substring(14,16))) return 1;
  //seconds
  if(parseInt(getText(obj2.date).substring(17,19))>parseInt(getText(obj1.date).substring(17,19))) return -1;
  else if(parseInt(getText(obj2.date).substring(17,19))<parseInt(getText(obj1.date).substring(17,19))) return 1;
  return 0;
}
interface IState
{
  count: number;
  index: number
}
const changeRead = (email: EmailProps) => {
  let i:number;
  for(i=0;i<TestData.length;i+=1)
  {
    if(email==TestData[i])
    {
      TestData[i].alreadyRead = !TestData[i].alreadyRead;
    }
  }
}
let myIndex = 1
let myEmaillll: EmailProps ={};
const changeEmail = (email:EmailProps) => {
  let i:number;
  for(i=0;i<TestData.length;i+=1)
  {
    if(email.date===TestData[i].date)
    {
      myIndex=i;
      myEmaillll=TestData[myIndex];
    }
  }
  
}

class EmailCard extends React.Component<EmailProps,IState>
{
  constructor(props:EmailProps) {
    super(props);
    this.state = {
      count: props.alreadyRead? 0:1,
      index: 1
    };
  }
  render(){ 
    const email:EmailProps = this.props;
    let eRead = email.alreadyRead;
    return(
      <div >
        <ListItem button onClick={()=>{this.setState({index: myIndex});changeEmail(email)}} style={{display: 'flex ', flexWrap: 'wrap',backgroundColor: this.state.count%2==0 ? 'lightgrey' : 'white',}}  >
          <div style={{display: 'flex '}}>
            <ListItemAvatar>
              <Avatar alt={getText(email.author)} src="/static/images/avatar/1.jpg" />
            </ListItemAvatar>
            <Button onClick={() => {this.setState({ count: this.state.count+1 }); changeRead(email)} }  style={{backgroundColor: this.state.count%2==0 ? 'white' : 'lightgrey'} }>
              {this.state.count%2==0? "seen" : "not seen"}
            </Button>
          </div>
         
          <ListItemText
            primary={getText(email.title)}
            secondary={
              <React.Fragment>
                <Typography
                  component="span"
                  variant="body2"
                  style={{display: 'inline', }}
                  color="textPrimary"
                >
                {getText(email.author)}
                </Typography>
                :  {getText(email.text).substring(0,40)+"..."}
              </React.Fragment>              
            }
          />
          <div style={{alignSelf: 'center' ,fontSize: '12px'}}>
          {getText(email.date)}
          </div>
       </ListItem>
       <Divider />
      </div>
      
    )
  }
}
class EmailContent extends React.Component<EmailProps>
{
  constructor(props:EmailProps ) { 
    super(props)
  }
  render()
  {
    const email:EmailProps = this.props;//{title: "Brunch this weekend?", author: "Ali Connors", date: d, text: "Witam  prosze poprawic mi ocene na 5 \n pozdrawiam" };
    return(
      <div style={{ height: '100%',width: '100%'}}>
        <header style={{alignSelf: 'center',fontWeight: 'bold', fontSize: '1.5rem',marginLeft: '5%',marginBottom: '2%'}}>
          {email.title}
        </header>
        <div style={{display: 'flex', height: '90%',justifyContent: 'felxstart',}}>          
          <ListItemAvatar >
              <Avatar alt={email.author} src="/static/images/avatar/1.jpg" />
          </ListItemAvatar>
          <div className="TextSections" style={{display: 'flex', flexDirection: 'column', width: '100%'}}>
            <div style={{fontWeight: 'bold', fontSize: '1 rem',height: '10%'}}>
              {email.author}
            </div>
            <div style={{height: '85%', width: '100%', overflowY: 'scroll'}}>
              {email.text}
            </div>   
            <div style={{height: '5%'}}>
              <Button startIcon={<ReplyIcon/>} style={{border: 'solid',marginRight: '3%',borderWidth: '2px', borderColor: 'lightgrey'}}>
                Odpowiedz
              </Button>
              <Button startIcon={<ArrowForwardIcon/>} style={{border: 'solid',borderWidth: '2px',borderColor: 'lightgrey'}}>
               Przekaż dalej
              </Button>
            </div>         
          </div>
        </div>
        
      </div>
    )
  }
}

export default function Inbox() {
  const [counter, SetCounter] = React.useState(0);
  const iCounter = ()=>SetCounter(counter+1);
  // const [message,SetMessage] = useState<EmailProps>();
  const [selectedIndex, setSelectedIndex] = React.useState(1);
  const handleListItemClick = (
    index: number,
  ) => {
    setSelectedIndex(index);
  };
  let myEmails: EmailProps[] = TestData;
  return (
    <div style={{display: 'flex', height: '87%'}}>
     <div style={{width: '28%'}} >
       <div className="buttony na sortowanie" style={{backgroundColor: 'lightgrey'}}>
            Date sorting:
         <Button onClick={iCounter} style={{width: '50%'}}>
           {counter%2==0 ? "descending" : "ascending"}
         </Button>
         <IconButton>
           <DateRangeIcon aria-label="choose date"/>
         </IconButton>
       </div>    
       <List style={{ overflowY: 'scroll',height: '94%', width: '100%'}} onClick={() => handleListItemClick(myIndex)}>
        {counter%2==0 ?TestData.sort(sortDescendingDate).map(email => <EmailCard {...email}/>):TestData.sort(sortAscendingDate).map(email => <EmailCard {...email}/>)}
       </List>       
     </div>
     <div style={{width: '2%', backgroundColor: 'white'}}>
     </div>
     <div style={{width: '70%', marginTop: '2%'}}>      
       <EmailContent {...myEmaillll}/>
     </div>
    </div>
  )
}
