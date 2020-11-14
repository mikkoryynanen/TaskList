<p align="center">
  <h1 align="center"><b>TaskList.API</b></h1>
  <p align="center">
    Simple task list RESTful Web API made with .net core 3.1
  </p>
</p>


## Usage

### Commands for users:

#### CREATE user
```
HttpPost: /api/users/
```

#### GET user
```
HttpGet api/users/{userId}
```

#### For debugging only, Get all users
```
HttpGet api/users/all
```


### Commands for tasks
Tasks use TaskResourceManager class as a resource for some of the commands. This is set in the body of the Http request as Json

#### CREATE task:
```
HttpPost: /api/tasks
```

#### GET single tasks for user
```
HttpGet api/tasks/{userId}/{taskId}
```

#### GET all tasks for user
```
HttpGet api/tasks/{userId}
```

#### UPDATE task
```
HttpUpdate api/tasks
Body 
{
    "TaskId": "{taskId}",
    "UserId": "{userId}",
    "Description": "{taskName}"
}
```

#### DELETE task
```
HttpDelete api/tasks/{userId}/{taskId}
```