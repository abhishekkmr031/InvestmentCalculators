pipeline {
    agent any

    // Environment variables can be defined here if needed, e.g., for setting a specific .NET path
    // environment {
    //     DOTNET_HOME = 'C:\\Program Files\\dotnet'
    // }

    stages {
        stage('Checkout') {
            steps {
                // The git checkout is automatically performed for a Pipeline from SCM job
                echo 'Checking out code...'
            }
        }

        stage('Restore Dependencies') {
            steps {
                // Use the 'bat' command for Windows agents or 'sh' for Linux/macOS
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                // Build the entire solution. Use --configuration Release for a release build.
                sh 'dotnet build --no-restore --configuration Release'
            }
        }

        stage('Test') {
            steps {
                // Run tests, assuming you have a test project in your solution
                sh 'dotnet test --no-build --no-restore --configuration Release'
                
                // You can add logic here to publish test results if you have the JUnit plugin installed
                // e.g., junit '**/TestResults/*.trx'
            }
        }

        stage('Publish Artifacts') {
            steps {
                // Publish your web project to a directory named 'published'
                sh 'dotnet publish --no-restore --configuration Release -o published'

                // Archive the published output as a build artifact in Jenkins
                archiveArtifacts artifacts: 'published/**/*', fingerprint: true
            }
        }
    }

    // You can define post-build actions, such as sending notifications
    post {
        always {
            // For example, clean up the workspace
            cleanWs()
        }
        failure {
            echo 'Build failed. Sending notification...'
        }
        success {
            echo 'Build successful. Archiving artifacts...'
        }
    }
}
