pipeline {
    agent any

    environment {
        BUILD_CONFIGURATION = 'Release'
        SOLUTION_FILE = 'investment-calculator.sln'
}


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
                sh 'dotnet restore ${SOLUTION_FILE}'
            }
        }

        stage('Build') {
            steps {
                // Build the entire solution. Use --configuration Release for a release build.
                sh 'dotnet build ${SOLUTION_FILE} --no-restore --configuration ${BUILD_CONFIGURATION}'
            }
        }

        stage('Test') {
            steps {
                // Run tests, assuming you have a test project in your solution
                sh 'dotnet test ${BUILD_CONFIGURATION} --no-build --no-restore --configuration ${BUILD_CONFIGURATION}'
                
                // You can add logic here to publish test results if you have the JUnit plugin installed
                // e.g., junit '**/TestResults/*.trx'
            }
        }

        stage('Publish Artifacts') {
            steps {
                // Publish your web project to a directory named 'published'
                sh 'dotnet publish ${BUILD_CONFIGURATION} --no-restore --configuration ${BUILD_CONFIGURATION} -o published'

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
