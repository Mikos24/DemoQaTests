Feature: Login functionality
    As a user
    I want to log in to demoqa.com
    So that I can access my profile

    Scenario Outline: Login with different credentials
        Given I navigate to the login page
        When On Login page: I will try to login with user name '<username>' and password '<password>'
        Then On Login page: I am successfully logged in = '<logged_in>'

    Examples: Valid credentials
        | username | password     | logged_in |
        | testuser | Password123! | true      |

    Examples: Invalid credentials
        | username     | password      | logged_in |
        | invaliduser  | Password123!  | false     |
        | testuser     | wrongpassword | false     |
        | emptyuser    |               | false     |
        |              | Password123!  | false     |