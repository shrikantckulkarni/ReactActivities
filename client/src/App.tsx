import { List, ListItem, ListItemText, Typography } from "@mui/material";
import { useEffect, useState } from "react"
import axios from "axios";

function App() {
const [reactactivities,SetReactActivities] = useState<ReactActivity[]>([]);

useEffect(()=> {
  axios.get<ReactActivity[]>('https://localhost:5001/api/reactactivities')
    .then(response => SetReactActivities(response.data))
    .catch(error => console.log(error));
  return () => {}
},[])

  return (
    <>
     <Typography className="app" style={{color:'red'}}>React Activities</Typography>
      <List>
        {reactactivities.map((reactactivity) => (
          <ListItem key={reactactivity.id}>
            <ListItemText primary={reactactivity.title} />
          </ListItem>
        ))}

      </List>
    </>
  )
}

export default App
