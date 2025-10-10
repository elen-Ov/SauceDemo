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
        script {
          deleteDir()
        }
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
          sh "export PATH=\$PATH:/usr/local/share/dotnet:/opt/homebrew/bin && dotnet test --filter \"Category=${params.TEST_TAG}\" --logger:\"trx;LogFileName=test-result.trx\""
        }
    }
  }
  
  post {
    always {
      sh 'echo "JAVA_HOME: $JAVA_HOME"'
      sh 'java -version || echo "Java not found"'
          script {
          timeout(time: 5, unit: 'MINUTES') { 
            allure([
              includeProperties: false,
              jdk: '',
              results: [[path: 'SauceDemo/TestResults']],
              reportBuildPolicy: 'ALWAYS'
            ])
          }
      }
      archiveArtifacts artifacts: 'SauceDemo/TestResults/*.trx', allowEmptyArchive: true
      sh 'echo "Post finished"'
    }
    failure {
      echo 'Test run is failed!'
    }
    success {
      echo 'SUCCESS!!!'
    }
  }
}