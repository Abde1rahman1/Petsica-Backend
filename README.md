# Petsica Backend API

Petsica is a comprehensive platform for pet lovers, providing a social network, marketplace, and pet management services. This backend API is built with ASP.NET Core and follows a clean architecture pattern to ensure a modular, scalable, and maintainable codebase.

## Technologies Used

- **ASP.NET Core 8**: For building the web API.
- **Entity Framework Core 8**: For data access and management.
- **JWT (JSON Web Tokens)**: For secure authentication.
- **Clean Architecture**: To separate concerns and create a robust application structure.

## Project Structure

The solution is organized into the following projects:

- **`Petsica.API`**: The presentation layer, which contains the controllers that expose the API endpoints.
- **`Petsica.Core`**: The domain layer, which includes the core business logic, entities, and abstractions.
- **`Petsica.Infrastructure`**: The infrastructure layer, which handles data access, external services, and other implementation details.
- **`Petsica.Service`**: The application layer, which orchestrates the business logic and connects the presentation and infrastructure layers.
- **`Petsica.Shared`**: A shared library containing contracts, constants, and other shared resources.

## Table of Contents

- [Authentication](#authentication)
- [Account Management](#account-management)
- [Pets](#pets)
- [Community](#community)
- [Marketplace](#marketplace)
- [Dashboard](#dashboard)

---

## Authentication

Handles user registration, login, and token management.

- **POST** `/Auth/Login`: Authenticates a user and returns a JWT token.
- **POST** `/Auth/refresh`: Refreshes an expired JWT token.
- **POST** `/Auth/revoke-refresh-token`: Revokes a refresh token.
- **POST** `/Auth/registerUser`: Registers a new user.
- **POST** `/Auth/registerClinic`: Registers a new clinic.
- **POST** `/Auth/confirm-email`: Confirms a user's email address.
- **POST** `/Auth/resend-confirmation-email`: Resends the email confirmation link.
- **POST** `/Auth/forget-password`: Sends a password reset code to the user's email.
- **POST** `/Auth/reset-password`: Resets the user's password.

---

## Account Management

Manages user profiles and services.

- **GET** `/me`: Retrieves the authenticated user's profile information.
- **PUT** `/me/info`: Updates the authenticated user's profile information.
- **PUT** `/me/change-password`: Changes the authenticated user's password.
- **POST** `/me/AddSitterService`: Adds a new sitter service for the user.
- **GET** `/me/AllService`: Retrieves all services for the authenticated user.
- **POST** `/me/ChooesService`: Allows a user to choose a service.
- **GET** `/me/GetAllSitterService`: Retrieves all sitter services.
- **PUT** `/me/UpdateSitterService`: Updates a sitter service.
- **DELETE** `/me/DeleteSitterService/{serviceId}`: Deletes a sitter service.

---

## Pets

Manages pet profiles and their status for adoption or mating.

- **POST** `/api/Pets/AddPet`: Adds a new pet for the authenticated user.
- **GET** `/api/Pets/GetAllPets`: Retrieves all pets for the authenticated user.
- **POST** `/api/Pets/PetAdoptionToggle/{petId}`: Toggles the adoption status of a pet.
- **POST** `/api/Pets/PetMatingToggle/{petId}`: Toggles the mating status of a pet.
- **GET** `/api/Pets/GetPetProfil/{petId}`: Retrieves the profile of a specific pet.
- **GET** `/api/Pets/GetPetMatingList`: Retrieves a list of pets available for mating.
- **GET** `/api/Pets/GetPetAdoptionList`: Retrieves a list of pets available for adoption.
- **PUT** `/api/Pets/UpdatePet`: Updates a pet's information.
- **DELETE** `/api/Pets/DeletePet/{petId}`: Deletes a pet.

---

## Community

Handles social interactions, including posts, comments, likes, and follows.

### Posts

- **POST** `/api/Posts`: Creates a new post.
- **PUT** `/api/Posts/{postId}`: Updates an existing post.
- **GET** `/api/Posts`: Retrieves all posts.
- **GET** `/api/Posts/{postId}`: Retrieves a specific post by its ID.
- **POST** `/api/Posts/delete/{PostId}`: Deletes a post.

### Comments

- **POST** `/api/Comments/{PostID}`: Adds a comment to a post.
- **GET** `/api/Comments/{PostId}`: Retrieves all comments for a specific post.
- **PUT** `/api/Comments/{CommentId}`: Updates a comment.
- **POST** `/api/Comments/delete/{CommentId}`: Deletes a comment.

### Likes

- **POST** `/api/Likes/{PostId}`: Adds a like to a post.
- **DELETE** `/api/Likes/{postId}`: Removes a like from a post.
- **GET** `/api/Likes/{postId}`: Retrieves all users who liked a specific post.

### User Follows

- **POST** `/api/UserFollows/follow/{followedUserId}`: Follows a user.
- **DELETE** `/api/UserFollows/follow/{followedUserId}`: Unfollows a user.
- **GET** `/api/UserFollows/followers/{followedUserId}`: Retrieves all followers of a user.
- **GET** `/api/UserFollows/following/{UserId}`: Retrieves all users that a specific user is following.

---

## Marketplace

Manages the marketplace, including products, carts, and orders.

### Products

- **POST** `/api/Products/addProduct`: Adds a new product.
- **GET** `/api/Products`: Retrieves all products.
- **GET** `/api/Products/Seller/products`: Retrieves all products for the authenticated seller.
- **GET** `/api/Products/{category}`: Retrieves all products in a specific category.
- **GET** `/api/Products/details/{productId}`: Retrieves the details of a specific product.
- **PUT** `/api/Products/edit/{productId}`: Updates a product.
- **POST** `/api/Products/delete/{productId}`: Deletes a product.
- **POST** `/api/Products/soldout/{productId}`: Marks a product as sold out.

### Carts

- **POST** `/api/Carts/add`: Adds an item to the cart.
- **GET** `/api/Carts/items`: Retrieves all items in the cart.
- **PUT** `/api/Carts/update`: Updates an item in the cart.
- **DELETE** `/api/Carts/remove/{productId}`: Removes an item from the cart.
- **DELETE** `/api/Carts/clear`: Clears the entire cart.

### Orders

- **POST** `/api/Orders/create`: Creates a new order from the cart.
- **GET** `/api/Orders/userorders`: Retrieves all orders for the authenticated user.
- **GET** `/api/Orders/{orderId}`: Retrieves a specific order for the authenticated user.
- **GET** `/api/Orders/sellerorders`: Retrieves all orders for the authenticated seller.
- **GET** `/api/Orders/seller/{orderId}`: Retrieves a specific order for the authenticated seller.
- **PUT** `/api/Orders/complete/{orderId}`: Marks an order as completed.
- **PUT** `/api/Orders/admin/complete/{orderId}`: Marks an order as completed by an admin.
- **PUT** `/api/Orders/cancel/{id}`: Cancels an order.
- **PUT** `/api/Orders/admin/cancel/{orderId}`: Cancels an order by an admin.
- **GET** `/api/Orders/all`: Retrieves all orders for an admin.
- **GET** `/api/Orders/all/sellerorders`: Retrieves all seller orders for an admin.


## Dashboard

Provides analytics and overview data for administrators.

- **GET** `/api/Dashboards/GetNumberUsers`: Retrieves the number of users by role.
- **GET** `/api/Dashboards/GetGeneralInfo`: Retrieves general information about the platform.
- **GET** `/api/Dashboards/GetNumberOfConfirmidEmail`: Retrieves statistics on email confirmations.
- **GET** `/api/Dashboards/GetAllTimeUserActivity`: Retrieves all-time user activity.
- **GET** `/api/Dashboards/GetTopContributors`: Retrieves the top contributors in the community.
- **GET** `/api/Dashboards/GetTopPosts`: Retrieves the top posts in the community.
- **GET** `/api/Dashboards/overview`: Retrieves an overview of the marketplace.
- **GET** `/api/Dashboards/GetTopSellingProducts`: Retrieves the top-selling products.
- **GET** `/api/Dashboards/GetTopSellingSellers`: Retrieves the top-selling sellers.
- **GET** `/api/Dashboards/GetCategoriesWithMostProducts`: Retrieves categories with the most products.


  
  
  
