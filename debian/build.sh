#!/bin/bash

VERSION="v1.0.0"
OUTPUT_FILE_NAME="exptrack-linux-x64-$VERSION.deb"
mkdir --parents --verbose usr/bin/
echo "Copying package files..."
cp ../publish/linux-x64/exptrack ./usr/bin/exptrack
echo "Creating debian package..."
dpkg-deb --build . $OUTPUT_FILE_NAME
echo "Done!"