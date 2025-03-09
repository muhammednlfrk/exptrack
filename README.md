# Expense Tracker CLI

A simple command-line interface (CLI) tool to efficiently  track your personal expenses. This application was built following the [roadmap.sh](https://roadmap.sh/projects/expense-tracker) project guide.

## Table of Contents

- [About the Project](#about-the-project)
- [Features](#features)
- [Installation](#installation)
  - [For Debian-based Systems (.deb)](#for-debian-based-systems-deb)
  - [For Windows Systems (.msi)](#for-windows-systems-msi)
  - [For Other Systems](#for-other-systems)
- [Usage](#usage)
  - [Adding an Expense](#adding-an-expense)
  - [Viewing Expenses](#)
- [Contributing](#contributing)
- [License](#license)

## About the Project

Expense Tracker CLI is a straightforward command-line application designed to help users manage their personal finances by tracking expenses only.

## Features

- Add, edit, and delete expense entries.
- Get summaries of your expenses.

## Installation

### For Debian-based Systems (.deb)

1. **Download**: Obtain the latest `.deb` package from the [releases page](https://github.com/muhammednlfrk/exptrack/releases).

2. **Install**: Open a terminal and run the following command:

   ```bash
   sudo dpkg -i /path/to/downloaded_file.deb
   ```

   Replace `/path/to/downloaded_file.deb` with the actual path to the downloaded `.deb` file.

3. **Dependencies**: If there are missing dependencies, resolve them by running:

   ```bash
   sudo apt-get install -f
   ```

### For Windows Systems (.msi)

1. **Download**: Get the latest `.msi` installer from the [releases page](https://github.com/muhammednlfrk/exptrack/releases).

2. **Install**: Double-click the downloaded `.msi` file and follow the on-screen instructions to complete the installation.

### For Other Systems

1. **Download**: Access the appropriate executable for your system from the [releases page](https://github.com/muhammednlfrk/exptrack/releases).

2. **Permissions**: Ensure the downloaded file has execute permissions. On Unix-based systems, you can set this with:

   ```bash
   chmod +x /path/to/downloaded_file
   ```

3. **Run**: Execute the application:

   ```bash
   ./path/to/downloaded_file
   ```

   Replace `/path/to/downloaded_file` with the actual path to the downloaded executable.

## Usage

After installation, you can use the `exptrack` command to manage your expenses.

### Adding an Expense

To add a new expense, use the following command:

```bash
exptrack add --amount 50 --description "Lunch at cafe"
```

### Listing Expenses

To view all recorded expenses, execute:

```bash
exptrack list
```

This will display a list of all expenses with their details.

## Contributing

Contributions are welcome! To contribute:

1. Fork the repository.

2. Create a new branch:

   ```bash
   git checkout -b feature/YourFeatureName
   ```

3. Commit your changes:

   ```bash
   git commit -m 'Add some feature'
   ```

4. Push to the branch:

   ```bash
   git push origin feature/YourFeatureName
   ```

5. Open a pull request detailing your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.
