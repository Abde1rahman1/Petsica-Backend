### Petsica Backend endpoints
### Community endpoints
# Endpoints
## Post
### **Add a Post**
- **Method**: `POST`
- **Route**: `/api/posts`
- **Description**: Adds a new post.
- **Request Body**:
  ```json
  {
    "content": "This is the content of the post."
  }
- **Response**: `201 Created` or `Bad Request`
### **Update Post**
- **Method**: `PUT`
- **Route**: `/api/posts/{PostId}`
- **Description**: Update post.
- **Request Body**:
  ```json
  {
    "content": "This is the content of the post."
  }
- **Response**: `204 Created` or `Bad Request`
### **Get All Post**
- **Method**: `GET`
- **Route**: `/api/posts`
- **Description**: return all post.
- **Response**: List of (postId , Content, UserId, Date, photo, LikesCount, CommentCount)
### **Get Post**
- **Method**: `GET`
- **Route**: `/api/posts/{PostId}`
- **Description**: return specific post.
- **Response**: List of (postId, Content, UserId, Date, photo, LikesCount, CommentCount)
### **Delete Post**
- **Method**: `POST`
- **Route**: `/api/posts/delete/{PostId}`
- **Description**: delete specific post.
- **Response**: 200 OK

## Comments
### **Add a Comment**
- **Method**: `POST`
- **Route**: `/api/Comments/{postId}`
- **Description**: Adds a new comment.
-  **Request Body**:
  ```json
  {
    "content": "This is the content of the post."
  }
```

- **Response Body**:
  200 OK
  ```json
  {
  "CommentId": "1",
  "UserID":"2",
  "Content":"content",
  "Date":"12-12-2002"
  }
  ```
  ### **Get Comments by PostId**
  - **Method**: `GET`
- **Route**: `/api/Comments/{postId}`
- **Response Body**:
  200 OK
    List of => 
  ```json
  {
  "CommentId": "1",
  "UserID":"2",
  "Content":"content",
  "Date":"12-12-2002"
  }
  ```
  ### **Update Comment**
- **Method**: `PUT`
- **Route**: `/api/Comments/{CommentId}`
- **Description**: Update comment.
- **Request Body**:
  ```json
  {
    "content": "This is the content of the post."
  }
- **Response Body**:
  200 OK
  ```json
  {
  "CommentId": "1",
  "UserID":"2",
  "Content":"content",
  "Date":"12-12-2002"
  }
  ```
  ### **Delete Post**
- **Method**: `POST`
- **Route**: `/api/posts/delete/{PostId}`
- **Description**: delete specific post.
- **Response**:   200 OK
  ```json
  {
  "CommentId": "1",
  "UserID":"2",
  "Content":"content",
  "Date":"12-12-2002"
  }

  
  
  
