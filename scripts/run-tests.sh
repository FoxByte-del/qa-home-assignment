#!/usr/bin/env bash
set -e

RED="\033[0;31m"
GREEN="\033[0;32m"
YELLOW="\033[1;33m"
BLUE="\033[0;34m"
NC="\033[0m"

IMAGE_NAME="cardvalidation-tests"

echo "🚀 Building Docker image..."
docker build -t $IMAGE_NAME -f Dockerfile.tests .

echo "🧪 Running tests inside Docker..."

docker run --rm $IMAGE_NAME bash -c '
set -e

RED="\033[0;31m"
GREEN="\033[0;32m"
YELLOW="\033[1;33m"
BLUE="\033[0;34m"
NC="\033[0m"

echo "======================"
echo "🧪 Running UNIT TESTS"
echo "======================"
dotnet test CardValidation.Core.Tests -c Release --no-build --logger "console;verbosity=detailed" \
  | grep -v -E "warn: Microsoft\.AspNetCore\.HttpsPolicy|Failed to determine the https port for redirect" \
  > /tmp/unit.log 2>&1 || true

echo "============================"
echo "🧪 Running INTEGRATION TESTS"
echo "============================"
dotnet test CardValidation.IntegrationTests -c Release --no-build --logger "console;verbosity=detailed" \
  | grep -v -E "warn: Microsoft\.AspNetCore\.HttpsPolicy|Failed to determine the https port for redirect" \
  > /tmp/integration.log 2>&1 || true

echo "============================"
echo "🧾 TEST SUMMARY"
echo "============================"

UNIT_SUMMARY=$(grep -E "Total tests:" /tmp/unit.log | tail -n 1 || echo "No summary found")
INT_SUMMARY=$(grep -E "Total tests:" /tmp/integration.log | tail -n 1 || echo "No summary found")

echo "🔹 Unit Tests:    $UNIT_SUMMARY"
echo "🔹 Integration Tests:    $INT_SUMMARY"

FAILED=$(grep -Eo "Failed:[[:space:]]*[1-9][0-9]*" /tmp/unit.log /tmp/integration.log | awk "{sum+=\$2} END {print sum+0}")
PASSED=$(grep -Eo "Passed:[[:space:]]*[0-9]+"       /tmp/unit.log /tmp/integration.log | awk "{sum+=\$2} END {print sum+0}")
SKIPPED=$(grep -Eo "Skipped:[[:space:]]*[0-9]+"      /tmp/unit.log /tmp/integration.log | awk "{sum+=\$2} END {print sum+0}")

# Function to display failed tests from a file
show_failed_tests() {
  local file="$1"
  local project="$2"

  local count
  count=$(grep -c "^  Failed " "$file" || true)
  if [ "$count" -gt 0 ]; then
    echo ""
    echo -e "${RED}❌ FAILED ${project} TESTS:${NC}"
    echo "---------------------------------------------"
    grep -A5 -E "^  Failed " "$file" \
      | grep -v -E "warn:|info:|Standard Output Messages|Stack Trace|at " \
      | sed "s/^/   /"
    echo "---------------------------------------------"
  fi
}

if [ "$FAILED" -gt 0 ]; then
  show_failed_tests /tmp/unit.log "UNIT"
  show_failed_tests /tmp/integration.log "INTEGRATION"
fi

echo "============================"
if [ "$FAILED" -gt 0 ]; then
  echo -e "${RED}💥 ${FAILED} failed${NC}, ${GREEN}${PASSED} passed${NC}, ${YELLOW}${SKIPPED} skipped${NC}"
  echo -e "${RED}❌ Some tests failed — see above.${NC}"
  exit 1
else
  echo -e "${GREEN}✅ ${PASSED} passed${NC}, ${YELLOW}${SKIPPED} skipped${NC}, ${RED}${FAILED} failed${NC}"
  echo -e "${GREEN}🎉 All tests passed successfully!${NC}"
fi
'
