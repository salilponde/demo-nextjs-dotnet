'use client'

import { useEffect, useState } from 'react'
import process from 'process'

export default function Home() {
  let [users, setUsers] = useState([])

  useEffect(() => {
    fetch(`${process.env.NEXT_PUBLIC_API_URL}/users`)
      .then(response => response.json())
      .then(data => setUsers(data))
  }, []);

  return (
    <div>
	  <h1>Welcome</h1>
      <ul>
        {users.map(item => <li key={item.email}>{item.email}</li>)}
      </ul>
    </div>
  )
}
