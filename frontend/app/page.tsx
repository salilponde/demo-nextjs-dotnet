'use client'

import { useEffect, useState } from 'react'
import process from 'process'

export default function Home() {
  let [users, setUsers] = useState([])

  useEffect(() => {
    fetch(`${process.env.NEXT_PUBLIC_API_URL}/users`)
      .then(response => response.json())
      .then(data => setUsers(data))
  });

  return (
    <div>
      <ul>
        {users.map(item => <li>{item.email}</li>)}
      </ul>
    </div>
  )
}
