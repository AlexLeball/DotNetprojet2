# DotNetprojet2

## Prerequisites

- A GitHub account
- Git installed on your local machine
- Basic knowledge of Git and command line
- .NET SDK installed (if applicable for this project)

## Steps to Fork a Repository

1. **Navigate to the Repository**
   - Go to the GitHub page of the repository you want to fork.

2. **Fork the Repository**
   - Click the **Fork** button in the upper right corner of the page.
   - This will create a copy of the repository in your own GitHub account.

## Steps to Clone a Repository

1. **Go to Your Forked Repository**
   - Navigate to your GitHub profile and find the forked repository.

2. **Copy the Repository URL**
   - Click the green **Code** button.
   - Copy the URL provided (either HTTPS or SSH).

3. **Open Your Terminal**
   - Open your command line interface (Terminal, Command Prompt, etc.).

4. **Clone the Repository**
   - Use the following command to clone the repository to your local machine:
     ```bash
     git clone https://github.com/AlexLeball/DotNetprojet2.git
     ```

5. **Navigate into the Cloned Directory**
   - Change into the directory of the cloned repository:
     ```bash
     cd DotNetprojet2
     ```

## Pulling Changes from the Original Repository

1. **Add the Original Repository as a Remote**
   - This step ensures you can pull updates from the original repository:
     ```bash
     git remote add upstream <https://github.com/AlexLeball/DotNetprojet2.git>
     ```


2. **Pull Changes**
   - To fetch and merge changes from the original repository:
     ```bash
     git pull upstream dev
     ```

## Conclusion

This guide has detailed the basic functionality of Git and GitHub, including forking, cloning, pulling changes, and pushing updates. You are now ready to work with the DotNetprojet2 repository on your local machine. Happy coding!
