### Petsica Backend endpoints
### Community endpoints
## Endpoints

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
- **Response**: `201 Created` or `Bad Request`
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
- **Response**: List of (postId, Content, UserId, Date, photo, LikesCount, CommentCount)
