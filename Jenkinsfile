pipeline {
  agent any
  
  stage('Check PATH') {
      steps {
          sh 'echo "PATH: $PATH"'
          sh 'which dotnet || echo "dotnet not found"'
          sh 'which allure || echo "allure not found"'
      }
  }
  
  parameters {
	string(
	  name:'TEST_TAG',
	  defaultValue: 'QA',
	  description: 'Run tests with tag'
	)
  }
  
  stages {
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
	    sh 'dotnet restore'
	  }
    }

    stage('Build') {
      steps {
        sh 'dotnet build --configuration Release'
      }
    }

    stage('Test') {
      steps {
        sh "dotnet test --filter \"Category=${params.TEST_TAG}\" --logger:\"trx;LogFileName=test-result.trx\""
      }
    }	

  }
  
  post {
    always {
	  sh 'allure generate TestResults --clean -o allure-report'
	  script {
	    allure([
      includeProperties: false,
      jdk: '',
      results: [[path: 'TestResults']],
      reportBuildPolicy: 'ALWAYS'
    ])
	  }
	  archiveArtifacts artifacts: '**/*.trx', allowEmptyArchive: true
	}
    failure {
      echo 'Test run is failed!'
    }
    success {
      echo 'SUCCESS!!!'
    }
  }
}