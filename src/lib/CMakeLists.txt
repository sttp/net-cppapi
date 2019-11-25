#******************************************************************************************************
#  CMakeLists.txt - Gbtc
#
#  Copyright © 2019, Grid Protection Alliance.  All Rights Reserved.
#
#  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
#  the NOTICE file distributed with this work for additional information regarding copyright ownership.
#  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may not use this
#  file except in compliance with the License. You may obtain a copy of the License at:
#
#      http://opensource.org/licenses/MIT
#
#  Unless agreed to in writing, the subject software distributed under the License is distributed on an
#  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
#  License for the specific language governing permissions and limitations.
#
#  Code Modification History:
#  ----------------------------------------------------------------------------------------------------
#  11/24/2019 - J. Ritchie Carroll
#       Generated original version of source code.
#
#******************************************************************************************************

cmake_minimum_required (VERSION 3.9)
project (sttp.net.lib VERSION 1.0.9 DESCRIPTION ".NET STTP C++ API Wrapper Library")

set (CMAKE_RUNTIME_OUTPUT_DIRECTORY Output)
set (CMAKE_ARCHIVE_OUTPUT_DIRECTORY Libraries)
set (CMAKE_LIBRARY_OUTPUT_DIRECTORY Libraries)

set (STTP_SOURCE_FILES
    "cppapi/src/lib/CommonTypes.cpp"
    "cppapi/src/lib/Convert.cpp"
    "cppapi/src/lib/EndianConverter.cpp"
    "cppapi/src/lib/pugixml.cpp"
    "cppapi/src/lib/data/DataColumn.cpp"
    "cppapi/src/lib/data/DataRow.cpp"
    "cppapi/src/lib/data/DataSet.cpp"
    "cppapi/src/lib/data/DataTable.cpp"
    "cppapi/src/lib/filterexpressions/ExpressionTree.cpp"
    "cppapi/src/lib/filterexpressions/FilterExpressionParser.cpp"
    "cppapi/src/lib/filterexpressions/FilterExpressionSyntaxBaseListener.cpp"
    "cppapi/src/lib/filterexpressions/FilterExpressionSyntaxLexer.cpp"
    "cppapi/src/lib/filterexpressions/FilterExpressionSyntaxListener.cpp"
    "cppapi/src/lib/filterexpressions/FilterExpressionSyntaxParser.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/ANTLRErrorListener.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/ANTLRErrorStrategy.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/ANTLRFileStream.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/ANTLRInputStream.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/BailErrorStrategy.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/BaseErrorListener.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/BufferedTokenStream.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/CharStream.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/CommonToken.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/CommonTokenFactory.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/CommonTokenStream.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/ConsoleErrorListener.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/DefaultErrorStrategy.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/DiagnosticErrorListener.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/Exceptions.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/FailedPredicateException.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/InputMismatchException.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/IntStream.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/InterpreterRuleContext.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/Lexer.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/LexerInterpreter.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/LexerNoViableAltException.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/ListTokenSource.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/NoViableAltException.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/Parser.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/ParserInterpreter.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/ParserRuleContext.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/ProxyErrorListener.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/RecognitionException.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/Recognizer.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/RuleContext.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/RuleContextWithAltNum.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/RuntimeMetaData.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/Token.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/TokenSource.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/TokenStream.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/TokenStreamRewriter.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/UnbufferedCharStream.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/UnbufferedTokenStream.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/Vocabulary.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/WritableToken.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ATN.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ATNConfig.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ATNConfigSet.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ATNDeserializationOptions.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ATNDeserializer.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ATNSerializer.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ATNSimulator.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ATNState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/AbstractPredicateTransition.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ActionTransition.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/AmbiguityInfo.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ArrayPredictionContext.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/AtomTransition.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/BasicBlockStartState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/BasicState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/BlockEndState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/BlockStartState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ContextSensitivityInfo.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/DecisionEventInfo.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/DecisionInfo.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/DecisionState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/EmptyPredictionContext.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/EpsilonTransition.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ErrorInfo.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LL1Analyzer.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerATNConfig.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerATNSimulator.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerAction.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerActionExecutor.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerChannelAction.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerCustomAction.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerIndexedCustomAction.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerModeAction.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerMoreAction.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerPopModeAction.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerPushModeAction.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerSkipAction.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LexerTypeAction.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LookaheadEventInfo.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/LoopEndState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/NotSetTransition.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/OrderedATNConfigSet.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ParseInfo.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ParserATNSimulator.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/PlusBlockStartState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/PlusLoopbackState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/PrecedencePredicateTransition.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/PredicateEvalInfo.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/PredicateTransition.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/PredictionContext.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/PredictionMode.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/ProfilingATNSimulator.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/RangeTransition.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/RuleStartState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/RuleStopState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/RuleTransition.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/SemanticContext.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/SetTransition.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/SingletonPredictionContext.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/StarBlockStartState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/StarLoopEntryState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/StarLoopbackState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/TokensStartState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/Transition.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/atn/WildcardTransition.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/dfa/DFA.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/dfa/DFASerializer.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/dfa/DFAState.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/dfa/LexerDFASerializer.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/misc/InterpreterDataReader.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/misc/Interval.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/misc/IntervalSet.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/misc/MurmurHash.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/misc/Predicate.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/support/Any.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/support/Arrays.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/support/CPPUtils.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/support/StringUtils.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/support/guid.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/ErrorNode.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/ErrorNodeImpl.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/IterativeParseTreeWalker.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/ParseTree.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/ParseTreeListener.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/ParseTreeVisitor.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/ParseTreeWalker.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/TerminalNode.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/TerminalNodeImpl.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/Trees.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/pattern/Chunk.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/pattern/ParseTreeMatch.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/pattern/ParseTreePattern.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/pattern/ParseTreePatternMatcher.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/pattern/RuleTagToken.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/pattern/TagChunk.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/pattern/TextChunk.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/pattern/TokenTagToken.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/xpath/XPath.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/xpath/XPathElement.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/xpath/XPathLexer.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/xpath/XPathLexerErrorListener.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/xpath/XPathRuleAnywhereElement.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/xpath/XPathRuleElement.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/xpath/XPathTokenAnywhereElement.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/xpath/XPathTokenElement.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/xpath/XPathWildcardAnywhereElement.cpp"
    "cppapi/src/lib/filterexpressions/antlr4-runtime/tree/xpath/XPathWildcardElement.cpp"
    "cppapi/src/lib/transport/ActiveMeasurementsSchema.cpp"
    "cppapi/src/lib/transport/CompactMeasurement.cpp"
    "cppapi/src/lib/transport/Constants.cpp"
    "cppapi/src/lib/transport/DataPublisher.cpp"
    "cppapi/src/lib/transport/DataSubscriber.cpp"
    "cppapi/src/lib/transport/MetadataSchema.cpp"
    "cppapi/src/lib/transport/PublisherInstance.cpp"
    "cppapi/src/lib/transport/RoutingTables.cpp"
    "cppapi/src/lib/transport/SignalIndexCache.cpp"
    "cppapi/src/lib/transport/SubscriberConnection.cpp"
    "cppapi/src/lib/transport/SubscriberInstance.cpp"
    "cppapi/src/lib/transport/TransportTypes.cpp"
    "cppapi/src/lib/transport/tssc/TSSCEncoder.cpp"
    "cppapi/src/lib/transport/tssc/TSSCDecoder.cpp"
    "cppapi/src/lib/transport/tssc/TSSCPointMetadata.cpp"
)

add_definitions(
   -DSWIG 
   -DBOOST_CONFIG_SUPPRESS_OUTDATED_MESSAGE 
   -DBOOST_ASIO_ENABLE_CANCELIO 
   -DANTLR4CPP_STATIC 
)

# Define include directories
include_directories(
    "cppapi/src/lib/filterexpressions/antlr4-runtime"
)

# Define compile flags
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++17 -ftabstop=4 -finput-charset=UTF-8 -fextended-identifiers -Wno-attributes")
if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "Clang")
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -stdlib=libc++")
endif()

set(CMAKE_CXX_FLAGS_DEBUG "${CMAKE_CXX_FLAGS_DEBUG} -Wall")

# Build sttp.net.lib
add_library (sttp.net.lib SHARED ${STTP_SOURCE_FILES} sttp.net.lib/sttp.net.lib.cpp)
target_link_libraries (sttp.net.lib pthread m z bz2 boost_system boost_thread boost_date_time boost_iostreams)

set_target_properties(sttp.net.lib PROPERTIES VERSION ${PROJECT_VERSION})
