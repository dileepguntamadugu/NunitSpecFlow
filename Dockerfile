# Use the latest Ubuntu image as the base
FROM ubuntu:latest
# Install dotnet sdk 8.0
RUN apt-get update && apt-get install -y curl unzip wget default-jdk dotnet-sdk-8.0

# Set environment variables for Playwright to work in Docker
ENV PLAYWRIGHT_BROWSERS_PATH=/ms-playwright

# Create working directory
WORKDIR /app

# Copy project files into the Docker image
COPY . ./

# Install dependencies
RUN dotnet restore

# Install dependencies and Playwright browsers
RUN apt-get update && apt-get install -y nodejs npm \
    && npm install -g playwright \
    && npx playwright install-deps \
    && npx playwright install

# Install Allure Command Line tool
#RUN curl -sSL https://github.com/allure-framework/allure2/releases/download/2.32.0/allure-2.32.0.tgz -o allure.tar.gz \
#  && tar -xzvf allure.tar.gz \
#  && mv allure*/bin/allure /usr/local/bin/allure

RUN wget https://github.com/allure-framework/allure2/releases/download/2.32.0/allure_2.32.0-1_all.deb \
    && dpkg -i allure_2.32.0-1_all.deb

# Download the AzCopy binary (replace the URL with the correct download link)
RUN curl -L https://aka.ms/downloadazcopy-v10-linux -o azcopy_linux.tar.gz
#RUN curl -L https://aka.ms/downloadazcopy-v10-linux-arm64 -o azcopy_linux.tar.gz
# Extract the AzCopy binary
RUN tar -xzvf azcopy_linux.tar.gz
RUN mv azcopy_linux_a*/azcopy azcopy
RUN chmod +x azcopy run_tests.sh

# Run tests
ENTRYPOINT ["/bin/sh", "-c", " ./run_tests.sh"]