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
          sh "export PATH=\$PATH:/usr/local/share/dotnet:/opt/homebrew/bin && dotnet test --filter \"Category=${params.TEST_TAG}\" --logger:\"trx;LogFileName=test-result.trx\" --logger:\"nunit;LogFileName=test-result.xml\""
        }
    }
  }
  
  post {
    always {
      sh 'echo "Post started"'
          sh 'echo "JAVA_HOME: $JAVA_HOME"'
          sh 'which java'
          sh '/usr/libexec/java_home -V'
          sh 'ls -la /Library/Java/JavaVirtualMachines/'
          sh 'ls -la TestResults || echo "TestResults not found"'
          sh 'find . -name "*.trx" | head -10'
          sh 'find . -name "*.xml" | head -10'
          script {
            allure([
              includeProperties: false,
              jdk: '',
              results: [[path: 'SauceDemo/TestResults']],
              reportBuildPolicy: 'ALWAYS'
            ])
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