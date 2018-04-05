FROM schedule-dotnet:snapshot

LABEL maintainer="Tim Visee <timvisee@gmail.com>"

# Add the application files
ADD ./ /app

# Add the initialisation script, run it on start
ADD ./docker/containers/init /root/init
CMD ["/root/init"]
