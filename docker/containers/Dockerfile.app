FROM schedule-dotnet:latest

LABEL maintainer="Tim Visee <timvisee@gmail.com>"

# Add the initialisation script, run it on start
ADD ./init /root/init
CMD ["/root/init"]
