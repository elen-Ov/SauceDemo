pipeline {
    agent any

    parameters {
        string(name: 'TEST_TAG', defaultValue: 'QA', description: 'Run tests with tag')
        string(name: 'ALLURE_RESULTS_PATH', defaultValue: '/Users/eovcharova/RiderProjects/SauceDemo/SauceDemo/bin/Debug/net8.0/allure-results', description: 'Путь к папке с результатами Allure (JSON-файлы)')
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
                cleanWs()
            }
        }

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                sh 'export PATH=$PATH:/usr/local/share/dotnet:/opt/homebrew/bin && dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'export PATH=$PATH:/usr/local/share/dotnet:/opt/homebrew/bin && dotnet build --configuration Release'
            }
        }

        stage('Test') {
            steps {
                sh """
                export PATH=\$PATH:/usr/local/share/dotnet:/opt/homebrew/bin
                mkdir -p TestResults
                dotnet test --filter "Category=${params.TEST_TAG}" --logger "trx;LogFileName=TestResults/test-results.trx" --logger "allure;output=${params.ALLURE_RESULTS_PATH}"
                """
            }
        }
    }

    post {
        always {
            script {
                if (fileExists(params.ALLURE_RESULTS_PATH)) {
                    sh """
                    export PATH=$PATH:/opt/homebrew/bin
                    mkdir -p allure-report
                    allure generate ${params.ALLURE_RESULTS_PATH} --output allure-report --clean
                    """
                    archiveArtifacts artifacts: 'TestResults/*.trx, allure-report/**', allowEmptyArchive: true
                } else {
                    echo "Warning: ${params.ALLURE_RESULTS_PATH} directory not found!"
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