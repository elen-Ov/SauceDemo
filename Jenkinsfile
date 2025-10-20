pipeline {
    agent any

    parameters {
        string(
            name: 'TEST_TAG',
            defaultValue: 'QA',
            description: 'Run tests with tag'
        )
    }

    stages {
        stage('Check PATH') {
            steps {
                sh 'echo "PATH: $PATH"'
                sh 'which dotnet || echo "dotnet not found"'
                sh 'which allure || echo "allure not found"'
            }
        }

        stage('Clean') {
            steps {
                cleanWs()  // cleanWs() вместо deleteDir()
            }
        }

        stage('Checkout') {
            steps {
                checkout([
                    $class: 'GitSCM',
                    branches: [[name: '*/release/Lesson22']],  // явное указание ветки
                    extensions: [],
                    userRemoteConfigs: [[url: 'https://github.com/elen-Ov/SauceDemo.git']]
                ])
            }
        }

        stage('Restore') {
            steps {
                withEnv(["PATH+EXTRA=${tool 'dotnet'}/bin:${env.PATH}"]) {
                    sh 'dotnet restore'
                }
            }
        }

        stage('Build') {
            steps {
                withEnv(["PATH+EXTRA=${tool 'dotnet'}/bin:${env.PATH}"]) {
                    sh 'dotnet build --configuration Release'
                }
            }
        }

        stage('Test') {
            steps {
                withEnv(["PATH+EXTRA=${tool 'dotnet'}/bin:${env.PATH}"]) {
                    sh """
                    mkdir -p TestResults || true
                    dotnet test --filter "Category=${params.TEST_TAG}" --logger "trx;LogFileName=TestResults/test-results.trx"
                    """
                }
            }
        }
    }

    post {
        always {
            script {
                // Проверка существования TestResults перед генерацией Allure
                def testResultsExist = fileExists 'TestResults'
                if (testResultsExist) {
                    allure([
                        includeProperties: false,
                        jdk: '',
                        results: [[path: 'TestResults']],
                        reportBuildPolicy: 'ALWAYS'
                    ])
                    archiveArtifacts artifacts: 'TestResults/*.trx', allowEmptyArchive: true
                } else {
                    echo "Warning: TestResults directory not found!"
                }
            }
            sh 'echo "Post finished"'
        }
        failure {
            echo 'Test run failed!'
        }
        success {
            echo 'SUCCESS!!!'
        }
    }
}